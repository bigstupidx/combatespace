<?php 
        include("connect.php");
 
        $facebookID = $_GET['facebookId'];
	
		$sth = $dbh->query("SELECT * FROM users WHERE `facebookID` = '$facebookID'");
        $sth->setFetchMode(PDO::FETCH_ASSOC);
 
		$result = $sth->fetchAll();
		if(count($result) > 0) {
			foreach($result as $r) {
				echo ":" . $r['name'] . ":" . $r['id'] . ":" . $r['hiscore'] . ":" . $r['email'];
			}
		}
		
?>