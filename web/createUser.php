<?php 
	include("connect.php");
	
	$hash = $_GET['hash'];
	
	$username = $_GET['username'];	

	$style= $_GET['style'];	
	
	$facebookID = $_GET['facebookID']; 
	
	$realHash = md5($facebookID . $username .  $secretKey); 
	
	
	$sth = $dbh->query("SELECT * FROM users WHERE `facebookID` = '$facebookID'");	
	$sth->setFetchMode(PDO::FETCH_ASSOC);

	$result = $sth->fetchAll();

	if(count($result) == 0) 
	{		
		if($realHash == $hash) { 
			$sth = $dbh->prepare("INSERT INTO  `users` 
									(`username` ,`facebookID`, `p1`,`p2`,`p3`,`p4`, `style`)
									 VALUES 
									('$username', '$facebookID', '10', '10', '10', '10', '$style')");
			try {
				$sth->execute($_GET);
				$lastId = $dbh->lastInsertId();
				echo $lastId;			
			} catch(Exception $e) {
				echo 'error';
			}
		} 
		
	}
?>

