﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Data.Models
{
    public class Audio
    {
        public AudioFileReader Reader { get; set; }
        public WaveOutEvent Output { get; set; }
    }
}
