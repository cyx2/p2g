using Common.Dto;
using Flurl.Http;
using System;

namespace Peloton;

public static class Extensions
{
	public static void EnsurePelotonCredentialsAreProvided(this PelotonSettings settings)
	{
		// SessionId is now required since Peloton deprecated the login endpoint
		if (!string.IsNullOrWhiteSpace(settings.SessionId))
		{
			// SessionId is provided, Email/Password are optional (used for identification only)
			return;
		}

		// If no SessionId, Email and Password are still required for backward compatibility
		// (though login endpoint is deprecated, this helps with error messages)
		if (string.IsNullOrEmpty(settings.Email))
			throw new ArgumentException("Peloton Email must be set. Since Peloton deprecated the login endpoint, you must provide a SessionId instead. See documentation for details.", nameof(settings.Email));

		if (string.IsNullOrEmpty(settings.Password))
			throw new ArgumentException("Peloton Password must be set. Since Peloton deprecated the login endpoint, you must provide a SessionId instead. See documentation for details.", nameof(settings.Password));
	}

	public static IFlurlRequest WithCommonHeaders(this IFlurlRequest request)
	{
		return request
			.WithHeader("Peloton-Platform", "web"); // needed to get GPS points for outdoor activity in response
	}
}
