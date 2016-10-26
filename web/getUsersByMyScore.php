<?php
	include("connect.php");	
	
	if(isset($_GET['ids']))
	{
		$string = $_GET['ids'];
		
		$ids = explode(',',$string);
		
		$query_ids = "";
		
		foreach($ids as $i =>$key) {				
			$query_ids .= " `facebookID` = '" . $key . "' ";  
			if($i < count($ids)-1) $query_ids .= " || ";
		}
		
		$query = "SELECT * FROM users WHERE " . $query_ids . " ORDER BY `score` ASC LIMIT 50";
		$sth = $dbh->query($query);
	} else
	{
		$score = $_GET['score'];	 
		$sth = $dbh->query("SELECT * FROM users WHERE `score` >= '$score' ORDER BY `score` ASC LIMIT 20");
	}
	
	$sth->setFetchMode(PDO::FETCH_ASSOC);
	$result = $sth->fetchAll();
	$result2 = 0;
	
	if(count($result) < 20) 
	{
		$sth2 = $dbh->query("SELECT * FROM users WHERE `score` < '$score' ORDER BY `score` ASC LIMIT 20");
		$sth2->setFetchMode(PDO::FETCH_ASSOC);
		$result2 = $sth2->fetchAll();
	}
	if(count($result2) > 0) {
		foreach($result2 as $r) {
			echo ":" . $r['facebookID'] . ":" . trim($r['nick']) . ":" .  $r['score']. ":" . $r['p1']. ":" . $r['p2']. ":" . $r['p3']. ":" . $r['p4'] . ":" . $r['peleas_g']. ":" . $r['peleas_p']. ":" . $r['retos_g']. ":" . $r['retos_p'] . ":" . $r['style'] ."</n>";
		}
	}
	if(count($result) > 0) {
		foreach($result as $r) {
			echo ":" . $r['facebookID'] . ":" . trim($r['nick']) . ":" .  $r['score']. ":" . $r['p1']. ":" . $r['p2']. ":" . $r['p3']. ":" . $r['p4'] . ":" . $r['peleas_g']. ":" . $r['peleas_p']. ":" . $r['retos_g']. ":" . $r['retos_p'] . ":" . $r['style'] ."</n>";
		}
	}
	
?>