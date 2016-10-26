<?php 
        $hostname = '';
        $username = '';
        $password = '';
        $database = '';
 
        $secretKey = "ranlogic2008"; 
 
        try {
            $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
        } catch(PDOException $e) {
            echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
        }
		
?>

