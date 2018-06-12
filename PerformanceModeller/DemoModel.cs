using System;
using System.Collections.Generic;
using System.Linq;

namespace Performance.Models
{
	public class DemoModel : IPerformanceModel
	{
		private IEnumerable<double> samples;
		private Random rand;

		public DemoModel()
		{
			this.samples = new List<double>
			{
				2000,
				4000,
				4000,
				4000,
				4000,
				4000,
				4000,
				4000,
				4000,
				1000,
				3000,
				0,
				2000,
				4000,
				4000,
				4000,
				2000,
				3000,
				4000,
				1000,
				4000,
				2000
			};
			this.rand = new Random();
		}

		public double DrawTime()
		{
			return samples.ElementAt(rand.Next(samples.Count()));
		}
	}
}
