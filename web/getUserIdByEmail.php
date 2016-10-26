<?php 
        include("connect.php");
 
        $email = $_GET['email'];
	
		$sth = $dbh->query("SELECT * FROM users WHERE `email` = '$email'");
        $sth->setFetchMode(PDO::FETCH_ASSOC);
 
		$result = $sth->fetchAll();
		if(count($result) > 0) {
			foreach($result as $r) {
				echo ":" . $r['id'] . ":" . $r['username'] . ":"  . $r['password'] . ":" . $r['email'] . ":" . $r['achievements'];
			}
		}
		
?>