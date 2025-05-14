using System;
using System.Collections.Generic; 

public class CPHInline
{
	
	public string checkInID; 
	public int checkInCount; 
	public string userName;
	
	public bool Execute()
	{
		//get the userName of the person that checked in. 
		CPH.TryGetArg("user", out userName); 
		
		//set a global counter variable for the check ins, if there isn't one already. 
		checkInCount = CPH.GetGlobalVar<int>("globalCheckInCounter", false); 
		if(checkInCount == 0)
		{
			CPH.SetGlobalVar("globalCheckInCounter", 1, false); 
		}
		
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

		//Send the Check In Message! 
		CPH.SendMessage(userName + " checked in! You are the " + checkInCount + getOrdinalSuffix(checkInCount) + " person to do so"); 
		
		//Incrememnt the count, update the variable in streamer bot and update the reward title. 
		checkInCount++; 
		CPH.SetGlobalVar("globalCheckInCounter", checkInCount, false); 
		
		//the reward title could be anything! 
		CPH.UpdateRewardTitle(checkInID, "Check In #" + checkInCount);
		
		//Update the prompt of the reward to let the person know who was before them. 
		CPH.UpdateRewardPrompt(checkInID, userName + " checked in before you!");
		
		//Update the background color of the channel point redemption to be a random color. 
		//Could add some logic here 
		Random random = new Random();
		String randomHexColor = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9" 
		CPH.UpdateRewardBackgroundColor(checkInID, randomHexColor); 
		
		return true;
	}
	
	
	
	//a helper function to write the ordinal numbers
	private static string getOrdinalSuffix(int num)
	{
		string number = num.ToString();
		if (number.EndsWith("11")) return "th";
		if (number.EndsWith("12")) return "th";
		if (number.EndsWith("13")) return "th";
		if (number.EndsWith("1")) return "st";
		if (number.EndsWith("2")) return "nd";
		if (number.EndsWith("3")) return "rd";
		return "th";
	}
}
