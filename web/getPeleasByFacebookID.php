<?php
	include("connect.php");	
	
	$facebookID = $_GET['facebookID'];
	
	$query = "SELECT retador_facebookID,retado_facebookID,retador_username,retado_username,winner, UNIX_TIMESTAMP(timestamp) AS mydatefield_timestamp FROM peleas WHERE `retador_facebookID` = '$facebookID' OR `retado_facebookID` = '$facebookID' ORDER BY `timestamp` DESC LIMIT 100";
	
	//echo $query;
	
    $sth = $dbh->query($query);
	
	$sth->setFetchMode(PDO::FETCH_ASSOC);

	$result = $sth->fetchAll();
	if(count($result) > 0) {
		foreach($result as $r) {
			echo "+" . $r['retador_facebookID'] . "+" . $r['retado_facebookID'] . "+" .  trim($r['retador_username']). "+" . trim($r['retado_username']). "+" . $r['winner']. "+" . $r['mydatefield_timestamp']. "</n>";
		}
	}
?>