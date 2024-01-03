class User
{
    private string _role { get; set; }
    private string _username { get; set; }
    private string _password { get; set; }
    private bool _loggedIn { get; set; }

    public User(string role, string username, string password)
    {
        _role = role;
        _username = username;
        _password = password;
    }

    public string GetRole()
    {
        return _role;
    }

    public bool CheckCredentials(string username, string password)
    {
        if (username == _username && password == _password)
        {
            _loggedIn = true;
            return true;
        }
        return false;
    }
}