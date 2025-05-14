using System;
using System.Collections.Generic; 
public class CPHInline
{
	public string checkInID; 
	
	public bool Execute()
	{
		
		//get the Twitch Reward ID
		//Assumes you have a reward containing "Check In"
		List<TwitchReward> rewardList = CPH.TwitchGetRewards(); 
		foreach(TwitchReward reward in rewardList)
		{
			if(reward.Title.Contains("Check In"))
			{ 
				checkInID = reward.Id; 
			}
		}
		
		CPH.SetGlobalVar("globalCheckInCounter", 1, false); 
		CPH.UpdateRewardTitle(checkInID, "Check In #1"); 
		CPH.UpdateRewardPrompt(checkInID, "You are the first one!!"); 
		
		return true;
	}
}
