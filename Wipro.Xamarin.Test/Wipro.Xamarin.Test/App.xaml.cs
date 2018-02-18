﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wipro.Xamarin.Test.View;
using Xamarin.Forms;

namespace Wipro.Xamarin.Test
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new ListPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
