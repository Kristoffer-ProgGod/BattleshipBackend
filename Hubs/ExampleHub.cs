using Microsoft.AspNetCore.SignalR;

namespace BattleshipBackend.Hubs;

public class ExampleHub : Hub
{
    Dictionary<string, IClientProxy> _users = new();
    Dictionary<string, GameRoom> _games = new();

    /// <summary>
    /// Checks against the user dictionary to see if the username is already in use.
    /// </summary>
    /// <param name="username"> The username the user wants to use. </param>
    /// <returns></returns>
    public bool Register(string username)
    {
        //Checks if the username fulfills all rules
        bool AllowedUserName()
        {
            if(username == null || username.Length<1)
            {
                return false;
            }
            return true;
        }
        bool UsernameExists()
        {
            return _users.Any(user => user.Equals(username));
        }

        //if username exists generate a new one based on it
        if (!UsernameExists() && AllowedUserName())
        {
            _users.Add(username, Clients.Caller);
            return true;
        }
        return false;
    }

    public bool Shoot(int[,] shotCoordinates)
    {

    }

    public bool YouSunkMyBattleShip()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="EnemyName">The username to play against</param>
    public void CreateGameRoom(string roomName, string playerTwo)
    {
        GameRoom gameRoom = new GameRoom(roomName);
        gameRoom.PlayerOne = Clients.Caller;
    }
    public void JoinGameRoom(string roomName)
    {
        try
        {
            GameRoom gameRoom = _games[roomName];
        }
        catch (Exception ex)
        {

        }
        
    }


}
class GameRoom
{
    public string roomName { get; }
    public IClientProxy PlayerOne { get; set; }
    public IClientProxy PlayerTwo { get; set; }
    public GameRoom(string roomName)
    {
        this.roomName = roomName;
    }
}