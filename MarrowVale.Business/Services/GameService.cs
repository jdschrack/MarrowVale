using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarrowVale.Business.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IPrintService _printService;
        private readonly IGlobalItemsProvider _globalItemsProvider;
        private readonly IDrawingRepository _drawingRepository;
        private readonly IDrawingService _drawingService;
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IInputProcessingService _inputProcessingService;
        private readonly ITimeService _timeService;
        private readonly ILocationRepository _locationRepository;

        private readonly ICombatService _combatService;
        
        private Player Player { get; set; }
        private Game Game { get; set; }
        
        public GameService(ILoggerFactory loggerFactory, IPrintService printService, IGlobalItemsProvider globalItemsProvider,
            IDrawingRepository drawingRepository, IDrawingService drawingService, IGameSetupService gameSetupService, IGameRepository gameRepository,
            ICombatService combatService, IInputProcessingService inputProcessingService, IPlayerRepository playerRepository, ITimeService timeService,
            ILocationRepository locationRepository)
        {
            _logger = loggerFactory.CreateLogger<GameService>();
            _printService = printService;
            _globalItemsProvider = globalItemsProvider;
            _drawingRepository = drawingRepository;
            _drawingService = drawingService;
            _gameRepository = gameRepository;
            _combatService = combatService;
            _playerRepository = playerRepository;
            _inputProcessingService = inputProcessingService;
            _timeService = timeService;
            _locationRepository = locationRepository;
            Player = gameSetupService.Setup();

            var currentGame = _gameRepository.LoadGame(Player.GameSaveName);

            if(currentGame != null)
            {
                Game = currentGame;
            }
            else
            {
                var location = _locationRepository.GetLocation("Starting Village");
                Game = new Game(location);
            }           
        }

        public void Start()
        {
            var clockCancellationToken = startGameClock();
            var currentLocation = string.Empty;
            while (true)
            {             
                if(currentLocation != Game.CurrentLocation.Name)
                {
                    currentLocation = Game.CurrentLocation.Name;
                    enterLocation(Game.CurrentLocation);
                }

                var playerInput = _printService.ReadInput();
                var command = _inputProcessingService.ProcessInput(playerInput)?.ToUpper();

                if (command == "QUIT")
                {
                    break;
                }else if(command == "SAVE")
                {
                    saveGame(clockCancellationToken);
                }

            }
        }

        private CancellationTokenSource startGameClock()
        {
            var tokenSource = new CancellationTokenSource();
            var cancellableTask = Task.Run(() =>
            {
                while (true) {
                    if (tokenSource.IsCancellationRequested)
                    {
                        break;
                    }
                    Game.GameTime.IncrementTime();
                    Thread.Sleep(5 * 60 * 1000);
                }
            }, tokenSource.Token);

            return tokenSource;
        }

        private void saveGame(CancellationTokenSource token)
        {
            token.Cancel();

            var oldSave = Player.GameSaveName;
            Player.UpdateSaveFields();
            _gameRepository.SaveGame(Game, oldSave, Player.GameSaveName);
            _playerRepository.SavePlayers();
        }

        private void enterLocation(Location location)
        {
            _printService.ClearConsole();
            //startup of a location
            if (location.PlayersVisited.Contains(Player.Name))
            {
                //player has visited this area. Read different description
                //talk to different npcs
                if (_timeService.GetGameTime(Game.GameTime) == TimeEnum.Evening || _timeService.GetGameTime(Game.GameTime) == TimeEnum.Night)
                {
                    //Player has visited and night time
                    _printService.Print(location.GetLocationDescription(location.NightVisitedDescription));
                }
                else
                {
                    //player has visited and daytime
                    
                    _printService.Print(location.GetLocationDescription(location.DayVisitedDescription));
                }
            }
            else
            {
                location.PlayersVisited.Add(Player.Name);

                //player has not visited and night time
                if(_timeService.GetGameTime(Game.GameTime) == TimeEnum.Evening || _timeService.GetGameTime(Game.GameTime) == TimeEnum.Night)
                {                   
                    _printService.Print(location.GetLocationDescription(location.NightDescription));
                }
                else
                {
                    //player has not visited and day time
                    _printService.Print(location.GetLocationDescription(location.DayDescription));
                }                
            }
        }
    }
}
