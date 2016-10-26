<?php 
	include("connect.php");		
	
	$userID = $_GET['userID'];
	$total = $_GET['total'];
	$hash = $_GET['hash']; 

	$realHash = md5($userID . $total . $secretKey);
	
	if($realHash == $hash) { 
	
		$sth = $dbh->prepare("UPDATE `users` SET  `achievements` =  '$total' WHERE `id` = '$userID' ");
			
		try {
			$sth->execute($_GET);
			echo "si";
		} catch(Exception $e) {
			echo 'error';
		}
	} 
?>