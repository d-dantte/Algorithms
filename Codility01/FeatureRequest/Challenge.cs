using System.Collections.Generic;
using System.Linq;

namespace Codility01.FeatureRequest
{
	public class Challenge
	{
		public List<string> popularNFeatures(
			int numFeatures,
			int topFeatures,
			List<string> possibleFeatures,
			int numFeatureRequests,
			List<string> featureRequests)
		{
			var normalized = featureRequests
				.Select(request => request.ToLower())
				.ToList();

			var featureList = new List<Feature>();
			foreach(var featureName in possibleFeatures)
			{
				var feature = new Feature
				{
					Name = featureName,
					RequestCount = 0
				};

				var lowerName = featureName.ToLower();
				foreach(var request in normalized)
				{
					if (request.Contains(lowerName))
						feature.RequestCount += 1;
				}

				featureList.Add(feature);
			}

			return featureList
				.Where(feature => feature.RequestCount > 0)
				.OrderByDescending(feature => feature.RequestCount)
				.ThenBy(feature => feature.Name)
				.Select(feature => feature.Name)
				.Take(topFeatures)
				.ToList();
		}
	}

	public class Feature
	{
		public string Name { get; set; }
		public int RequestCount { get; set; }
	}
}
