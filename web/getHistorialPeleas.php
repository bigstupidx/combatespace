<?php 
        include("connect.php");
 
        $facebookID = $_GET['facebookID'];
		$facebookID2 = $_GET['facebookID2'];
	
		$sth = $dbh->query("SELECT * FROM peleas WHERE (`retador_facebookID` = '$facebookID' AND `retado_facebookID` = '$facebookID2') OR (`retador_facebookID` = '$facebookID2' AND `retado_facebookID` = '$facebookID')");
		
        $sth->setFetchMode(PDO::FETCH_ASSOC);
 
		$result = $sth->fetchAll();
		
		$ganadas_player1 = 0;
		$ganadas_player2 = 0;
		
		if(count($result) > 0) {
			foreach($result as $r) {
			
				if($r['winner'] == $facebookID) 
					$ganadas_player1++; 
				else 
					$ganadas_player2++;
				
				
			}
		} 
		echo $ganadas_player1 . "_" . $ganadas_player2;
		
?>