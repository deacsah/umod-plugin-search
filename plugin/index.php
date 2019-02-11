<?php

$plugins = scandir('/var/www/opperbazen.nl/public_html/rust/plugins');
foreach($plugins as $plugin) {
	echo $plugin."<br/>";
}
