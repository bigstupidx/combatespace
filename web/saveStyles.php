<?php 
	include("connect.php");		
	
	$facebookID = $_GET['facebookID'];
	$style = $_GET['style'];
	
	$hash = $_GET['hash']; 

	$realHash = md5($facebookID . $style . $secretKey);
	echo $realHash . " - " . $hash;
	
	if($realHash == $hash) { 
	
		$sth = $dbh->prepare("UPDATE `users` SET  `style` =  '$style' WHERE `facebookID` = '$facebookID' ");
			
		try {
			$sth->execute($_GET);
			echo "si";
		} catch(Exception $e) {
			echo 'error';
		}
	} 
?>