<?php 
ini_set('max_execution_time', 0);
$hooks = [
	'OnNpcTarget',
	'OnNpcPlayerTarget',
	'OnNpcPlayerResume',
	'OnSurveyGather',
	'OnFlameExplosion',
	'OnLootPlayer',
	'OnRefreshVendingStock',
	'OnFlameThrowerBurn',
	'OnFireBallDamage',
	'OnFireBallSpread',
	'CanBeTargeted',
	'OnItemRepair'
];
$plugins = scandir('/var/www/opperbazen.nl/public_html/rust/plugins');
foreach ($plugins as $plugin) {
	if (!in_array($plugin, ['.','..','index.php'])) {
		$content = file_get_contents("plugins/".$plugin);
		foreach ($hooks as $hook) {
			if (strpos($content, $hook) !== FALSE) {
				echo "FOUND! Plugin <strong>$plugin</strong> contains hook '".$hook."'<br/>";
			}
		}
	}
}
?>
