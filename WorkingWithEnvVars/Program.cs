SectionTitle("Reading all environment variables for proccess");
IDictionary vars = GetEnvironmentVariables();
DictionaryToTable(vars);

SectionTitle("Reading all environment variables for machine");
IDictionary machVars = GetEnvironmentVariables(EnvironmentVariableTarget.Machine);
DictionaryToTable(machVars);

SectionTitle("Reading all environment variables for user.");
IDictionary userVars = GetEnvironmentVariables(EnvironmentVariableTarget.User);

#region Expanding, setting, and getting an environment variables

/*
 * Scope Level         | Command
 * Session/Shell       | set MY_ENV_VAR="Alpha"
 * User                | setx MY_ENV_VAR "Beta"
 * Machine             | setx MY_ENV_VAR "Gamma" /M
 */

string myComputer = "My username is %USERNAME%. My CPU is %PROCESSOR_IDENTIFIER%.";

string password_key = "MY_PASSWORD";
SetEnvironmentVariable(password_key, "Pa$$w0rd");
string? password = GetEnvironmentVariable(password_key);
WriteLine($"{password_key}: {password}");

string secret_key = "MY_SECRET";
string? secret = GetEnvironmentVariable(secret_key, EnvironmentVariableTarget.Process);
WriteLine($"Process - {secret_key}: {secret}");

secret = GetEnvironmentVariable(secret_key, EnvironmentVariableTarget.User);
WriteLine($"User    - {secret_key}: {secret}");

secret = GetEnvironmentVariable(secret_key, EnvironmentVariableTarget.Machine);
WriteLine($"User    - {secret_key}: {secret}");

#endregion
