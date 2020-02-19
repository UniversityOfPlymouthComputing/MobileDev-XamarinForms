using System;
using System.Collections.Generic;
using uoplib.mvvm;
using Xamarin.Forms;


/*
 * The colors include white for command;
 * gold for engineering;
 * gray for science, communications and navigation;
 * dark green for security;
 * light green for medical;
 * dark blue for operations;
 * light blue for special services;
 * red for low-grade officers and officer cadets.
 *
 * Star Trek uniforms - Wikipediaen.wikipedia.org › wiki › Star_Trek_uniforms
 * (I wish I hadn't asked)
 */

namespace Commanding
{
    public partial class RankSelectionPage : ContentPage
    {
        public RankSelectionPage(ViewModelBase vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}
