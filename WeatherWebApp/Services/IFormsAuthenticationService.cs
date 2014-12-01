namespace WeatherWebApp.Services
{
	public interface IFormsAuthenticationService
	{
		void SignIn(string username);

		void SignOut();
	}
}