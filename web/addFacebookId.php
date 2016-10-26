<?php 
	include("connect.php");
	
	$id = $_GET['id'];
	$facebookID = $_GET['facebookId']; 
	$hash = $_GET['hash']; 

	$realHash = md5($id . $facebookID . $secretKey); 
	echo $realHash . " " . $hash;
	if($realHash == $hash) { 
		$sth = $dbh->prepare("UPDATE users set `facebookID` = '$facebookID' WHERE `id` = '$id'");
		echo "UPDATE users set `facebookID` = '$facebookID' WHERE `id` = '$id'";
		try {
			$sth->execute($_GET);
		} catch(Exception $e) {
			echo 'error';
		}
	} 
?>