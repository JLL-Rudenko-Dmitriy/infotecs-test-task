namespace EnvLoader;

public static class DotEnv
{
    public static void Load(string environmentFilePath)
    {
        if (!File.Exists(environmentFilePath))
        {
            throw new FileNotFoundException("Environment file not found", environmentFilePath);
        }

        var file = File.ReadLines(environmentFilePath);
        foreach (var line in file)
        {
            if (line.StartsWith('#') || string.IsNullOrEmpty(line))
            {
                continue;
            }

            var keyValue = line.Split('=');
            Environment.SetEnvironmentVariable(keyValue[0], keyValue[1]);
        }
    }

    public static void Load(string key, string value)
    {
        Environment.SetEnvironmentVariable(key, value);
    }
}