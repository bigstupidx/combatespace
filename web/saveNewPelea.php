<?php 
	include("connect.php");
	
	$hash = $_GET['hash'];
	
	$retador_username = $_GET['retador_username'];	 
	$retado_username = $_GET['retado_username'];	
	$retador_facebookID = $_GET['retador_facebookID']; 
	$retado_facebookID = $_GET['retado_facebookID']; 
	
	$winner = $_GET['winner'];
	//$round = $_GET['round'];
	
	$realHash = md5($retador_facebookID . $retado_facebookID . $winner . $secretKey); 
	
	//echo ($realHash . " - " . $hash);
	
	if($realHash == $hash) { 
		$query = "INSERT INTO `peleas` (`retador_username` ,`retado_username`, `retador_facebookID`,`retado_facebookID`,`winner`,`status`)
										VALUES 
									   ('$retador_username', '$retado_username', '$retador_facebookID', '$retado_facebookID', '$winner', '0')";
		$sth = $dbh->prepare($query);
		echo $query;
		try {
			$sth->execute($_GET);
			//$lastId = $dbh->lastInsertId();
			echo '  ready';			
		} catch(Exception $e) {
			echo 'error';
		}
	} 
?>

