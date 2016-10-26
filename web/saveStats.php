<?php 
	include("connect.php");		
	
	$facebookID = $_GET['facebookID'];
	$score = $_GET['score'];
	$stat1 = $_GET['stat1'];
	$stat2 = $_GET['stat2'];
	$stat3 = $_GET['stat3'];
	$stat4 = $_GET['stat4'];
	
	
	$hash = $_GET['hash']; 

	$realHash = md5($facebookID . $score . $stat1 . $stat2 . $stat3 . $stat4 . $secretKey);
	
	if($realHash == $hash) { 
	
		$sth = $dbh->prepare("UPDATE `users` SET  `score` =  '$score', `p1` =  '$stat1', `p2` =  '$stat2', `p3` =  '$stat3', `p4` =  '$stat4' WHERE `facebookID` = '$facebookID' ");
			
		try {
			$sth->execute($_GET);
			echo "si";
		} catch(Exception $e) {
			echo 'error';
		}
	} 
?>