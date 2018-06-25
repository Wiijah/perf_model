using System;
using System.Collections.Generic;
using System.Linq;

namespace Performance.Models
{
	public class LoginServiceModel : IPerformanceModel
	{
		private IEnumerable<double> samples;
		private Random rand;

		public LoginServiceModel()
		{
			this.samples = new List<double>
			{
				632,
				661,
				645,
				670,
				665,
				640,
				638,
				661,
				654,
				647
			};
			this.rand = new Random();
		}

		public double DrawTime()
		{
			return samples.ElementAt(rand.Next(samples.Count()));
		}
	}
}
