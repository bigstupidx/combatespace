<?php 
	include("connect.php");		
	
	$userID = $_GET['userID'];
	$filtered = $_GET['filtered'];
	$hash = $_GET['hash']; 

	$realHash = md5($userID . $filtered . $secretKey);
	
	if($realHash == $hash) { 
	
		$sth = $dbh->prepare("UPDATE `users` SET  `filtered` =  '$filtered' WHERE `id` = '$userID' ");
			
		try {
			$sth->execute($_GET);
			echo "filtered=" . $filtered;
		} catch(Exception $e) {
			echo 'error';
		}
	} 
?>