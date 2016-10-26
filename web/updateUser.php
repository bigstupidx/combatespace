<?php 
	include("connect.php");
	
	$hash = $_GET['hash'];	
	$facebookID = $_GET['facebookID'];
	$nick = $_GET['nick'];
		
	$realHash = md5($facebookID . $nick . $secretKey);
	
	$nick = mysql_escape_string ( $nick );
	
	if($realHash == $hash) { 
	
		$sth = $dbh->prepare("UPDATE `users` SET  `nick` =  '$nick' WHERE `facebookID` = '$facebookID' ");
		
		try {
			$sth->execute($_GET);
			$lastId = $dbh->lastInsertId();
			echo $lastId;			
		} catch(Exception $e) {
			echo 'error';
		}
	} 
?>