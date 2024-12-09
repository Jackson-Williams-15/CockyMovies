<?php
/**
* Simple Meta Refresh Redirect Plugin for Joomla Content
* Version			: 1.4
* Created by		: webGobe::webgobe.com
* Created on		: Dec 21th, 2015
* Updated on		: Feb 19th, 2022
* Package			: Joomla 3.x.x
* License			: http://www.gnu.org/copyleft/gpl.html GNU/GPL, see LICENSE.php
*/
 
// no direct access
defined('_JEXEC') or die('Restricted access');

jimport( 'joomla.event.plugin' );
	
class plgContentmetarefresh extends JPlugin 
{
/*	function plgContentmetarefresh(&$subject, $params){
		parent::__construct( $subject, $params );
 	}*/
	function __construct(& $subject, $config) {
        parent::__construct($subject, $config);
		
		jimport('joomla.plugin.plugin');
		$this->_params = $this->params;
	
		
    }

	
	function onContentBeforeDisplay( $context, &$article, $isNew )	{
		if ( !$this->params->get( 'enabled', 1 ) ) {
			return;
		}
		if ( !preg_match("#{MetaRefresh}(.*?){/MetaRefresh}#s", $article->text) )  {
                return;
            }  else  {
                $this->redirect($article->text);
                return;
            }
	}
    private function redirect(&$text){
		//$plugin	=JPluginHelper::getPlugin('content','metarefresh');
		$param = $this->params;
		
		// Get Params
		$target		= $this->params->get( 'target', '' ) ;
		$timeout	= $this->params->get( 'timeout', '0' ) ;
        	$regex = "#{MetaRefresh}(.*?){/MetaRefresh}#sU";
		$article_text = htmlspecialchars_decode($text);
	    if (preg_match_all($regex, $article_text, $matches, PREG_PATTERN_ORDER) > 0 ) {
            	$match = $matches[1][0];
				$url= $match;
				if (strpos($url,'www.')===0){
                	$url= 'http://'.$url;
            	}
				$custom_tag = '<meta http-equiv="refresh" content="'.$timeout.'; url='.$url.'" >'; 
				if($target) $custom_tag .= '<meta http-equiv="target" content="_blank">';
           	 	$document =JFactory::getDocument();
            	$document->addCustomTag($custom_tag);
				$text = preg_replace($regex, '' , $text );
            	}

	}
		
}
?>