<?php 
	include("connect.php");
	
	$userID = $_GET['id'];
	$levelID = $_GET['levelID'];
	$score = $_GET['score']; 
	$new = $_GET['new']; 
	$hash = $_GET['hash']; 

	$realHash = md5($userID . $levelID . $score . $secretKey); 
	if($realHash == $hash) { 
	
		if($new == "True")
		{
			$sth = $dbh->prepare("INSERT INTO  `level" . $levelID . "`
								(`userID` , `score`)
								 VALUES 
								('$userID',  '$score')");
		}
		else
		{
			$sth = $dbh->prepare("UPDATE `level" . $levelID . "` SET  `score` =  '$score' WHERE `userID` = '$userID' ");
		}
			
		try {
			$sth->execute($_GET);
		} catch(Exception $e) {
			echo 'error';
		}
	} 
?>