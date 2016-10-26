<?php 
	include("connect.php");
	
	$hash = $_GET['hash'];	
	$facebookID = $_GET['facebookID'];
	
	$peleas_g = $_GET['peleas_g'];
	$peleas_p = $_GET['peleas_p'];
	$retos_g = $_GET['retos_g'];
	$retos_p = $_GET['retos_p'];

	$realHash = md5($facebookID . $peleas_g . $peleas_p . $retos_g . $retos_p . $secretKey);
	
	$value = mysql_escape_string ( $value );
	
	if($realHash == $hash) { 
	
		$sth = $dbh->prepare("UPDATE `users` SET `peleas_g` =  '$peleas_g', `peleas_p` =  '$peleas_p', `retos_g` =  '$retos_g', `retos_p` =  '$retos_p' WHERE `facebookID` = '$facebookID' ");
		
		try {
			$sth->execute($_GET);
			$lastId = $dbh->lastInsertId();
			echo $lastId;			
		} catch(Exception $e) {
			echo 'error';
		}
	} 
?>