<?php
	include("connect.php");	
	
	$levelID = $_GET['levelID'];
 
    $sth = $dbh->query('SELECT l.userID as userID, l.score as score, u.facebookID as facebookID, u.name as name FROM level1 l, users u WHERE u.id = userID ORDER BY l.score DESC LIMIT 20');

    $sth->setFetchMode(PDO::FETCH_ASSOC);
 
    $result = $sth->fetchAll();
 
    if(count($result) > 0) {
        foreach($result as $r) {
            echo $r['userID'] .  ":" .  $r['score'] .  ":" .  $r['facebookID'] . ":" .  $r['name'] . "</n>";
        }
    }
?>