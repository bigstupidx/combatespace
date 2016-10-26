<?php
	include("connect.php");	
	
	$filtered = $_GET['filtered'];
 
 //   $sth = $dbh->query("SELECT * FROM users WHERE filtered = '$filtered' ORDER BY filtered ASC LIMIT 300");
    $sth = $dbh->query("SELECT * FROM users ORDER BY filtered ASC LIMIT 300");

    $sth->setFetchMode(PDO::FETCH_ASSOC);
 
    $result = $sth->fetchAll();
 
    if(count($result) > 0) {
        foreach($result as $r) {
            echo $r['id'] .  ":" .  $r['username'] .  ":" .  $r['password'] . ":" .  $r['email'] . ":" .  $r['achievements'] . ":" .  $r['filtered'] . "</n>";
        }
    }
?>