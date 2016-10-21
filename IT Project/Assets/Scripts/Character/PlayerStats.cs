using System;


public class PlayerStats {
    public string UserName;
    public int Kill;
    public int Death;

    public PlayerStats(string userName, int kill, int death) {
        this.UserName = userName;
        this.Kill = kill;
        this.Death = death;
    }
}