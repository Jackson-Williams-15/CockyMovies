<?php
/**
 * @package   akeebabackup
 * @copyright Copyright (c)2006-2023 Nicholas K. Dionysopoulos / Akeeba Ltd
 * @license   GNU General Public License version 3, or later
 */

namespace Akeeba\Backup\Admin\Model;

// Protect from unauthorized access
defined('_JEXEC') || die();

use Exception;
use FOF40\Container\Container;
use FOF40\Update\Update;
use Joomla\CMS\Factory as JFactory;
use Joomla\CMS\Filesystem\File;

/**
 * Updates model. Acts as an intermediary between the component and Joomla!,
 *
 * @package Akeeba\Backup\Admin\Model
 */
class Updates extends Update
{
	/**
	 * Obsolete update site locations
	 *
	 * @var  array
	 */
	protected $obsoleteUpdateSiteLocations = [
		'http://cdn.akeebabackup.com/updates/abpro.xml',
		'http://cdn.akeebabackup.com/updates/abcore.xml',
		'http://cdn.akeebabackup.com/updates/fof.xml',
	];

	/**
	 * Public constructor. Initialises the protected members as well.
	 *
	 * @param   array  $config
	 */
	public function __construct($config = [])
	{
		$container = Container::getInstance('com_akeeba');

		$config['update_component'] = 'pkg_akeeba';
		$config['update_sitename']  = 'Akeeba Backup Core';
		$config['update_site']      = 'https://cdn.akeeba.com/updates/pkgakeebacore.xml';
		$config['update_paramskey'] = 'update_dlid';
		$config['update_container'] = $container;

		$isPro = defined('AKEEBA_PRO') ? AKEEBA_PRO : 0;

		if ($isPro)
		{
			$config['update_sitename'] = 'Akeeba Backup Professional';
			$config['update_site']     = 'https://cdn.akeeba.com/updates/pkgakeebapro.xml';
		}

		if (defined('AKEEBA_VERSION') && !in_array(substr(AKEEBA_VERSION, 0, 3), ['dev', 'rev']))
		{
			$config['update_version'] = AKEEBA_VERSION;
		}

		parent::__construct($config);

		$this->container    = $container;
		$this->extension_id = $this->findExtensionId('pkg_akeeba', 'package');

		if (empty($this->extension_id))
		{
			$this->createFakePackageExtension();
			$this->extension_id = $this->findExtensionId('pkg_akeeba', 'package');
		}
	}

	/**
	 * Refreshes the update sites, removing obsolete update sites in the process
	 */
	public function refreshUpdateSite(): void
	{
		// Remove any update sites for the old com_akeeba package
		$this->removeObsoleteComponentUpdateSites();

		// Refresh our update sites
		parent::refreshUpdateSite();
	}

	/**
	 * Removes the obsolete update sites for the component, since now we're dealing with a package.
	 *
	 * Controlled by componentName, packageName and obsoleteUpdateSiteLocations
	 *
	 * Depends on getExtensionId, getUpdateSitesFor
	 *
	 * @return  void
	 */
	private function removeObsoleteComponentUpdateSites()
	{
		// Initialize
		$deleteIDs = [];

		// Get component ID
		$componentID = $this->findExtensionId('com_akeeba', 'component');

		// Get package ID
		$packageID = $this->findExtensionId('pkg_akeeba', 'package');

		// Update sites for old extension ID (all)
		if ($componentID)
		{
			// Old component packages
			$moreIDs = $this->getUpdateSitesFor($componentID, null);

			if (is_array($moreIDs) && count($moreIDs))
			{
				$deleteIDs = array_merge($deleteIDs, $moreIDs);
			}

			// Obsolete update sites
			$moreIDs = $this->getUpdateSitesFor(null, $componentID, $this->obsoleteUpdateSiteLocations);

			if (is_array($moreIDs) && count($moreIDs))
			{
				$deleteIDs = array_merge($deleteIDs, $moreIDs);
			}
		}

		// Update sites for any but current extension ID, location matching any of the obsolete update sites
		if ($packageID)
		{
			// Update sites for all of the current extension ID update sites
			$moreIDs = $this->getUpdateSitesFor($packageID, null);

			if (is_array($moreIDs) && count($moreIDs))
			{
				$deleteIDs = array_merge($deleteIDs, $moreIDs);
			}

			$deleteIDs = array_unique($deleteIDs);

			// Remove the last update site
			if (count($deleteIDs))
			{
				$lastID = array_pop($moreIDs);
				$pos    = array_search($lastID, $deleteIDs);
				unset($deleteIDs[$pos]);
			}
		}

		$db        = $this->container->db;
		$deleteIDs = array_unique($deleteIDs);

		if (empty($deleteIDs) || !count($deleteIDs))
		{
			return;
		}

		$deleteIDs = array_map([$db, 'q'], $deleteIDs);

		$query = $db->getQuery(true)
			->delete($db->qn('#__update_sites'))
			->where($db->qn('update_site_id') . ' IN(' . implode(',', $deleteIDs) . ')');

		try
		{
			$db->setQuery($query)->execute();
		}
		catch (Exception $e)
		{
			// Do nothing.
		}

		$query = $db->getQuery(true)
			->delete($db->qn('#__update_sites_extensions'))
			->where($db->qn('update_site_id') . ' IN(' . implode(',', $deleteIDs) . ')');

		try
		{
			$db->setQuery($query)->execute();
		}
		catch (Exception $e)
		{
			// Do nothing.
		}
	}

	/**
	 * Gets the ID of an extension
	 *
	 * @param   string  $element  Extension element, e.g. com_foo, mod_foo, lib_foo, pkg_foo or foo (CAUTION: plugin,
	 *                            file!)
	 * @param   string  $type     Extension type: component, module, library, package, plugin or file
	 * @param   null    $folder   Plugins: plugin folder. Modules: admin/site
	 *
	 * @return  int  Extension ID or 0 on failure
	 */
	private function findExtensionId($element, $type = 'component', $folder = null)
	{
		$db    = $this->container->db;
		$query = $db->getQuery(true)
			->select($db->qn('extension_id'))
			->from($db->qn('#__extensions'))
			->where($db->qn('element') . ' = ' . $db->q($element))
			->where($db->qn('type') . ' = ' . $db->q($type));

		// Plugin? We should look for a folder
		if ($type == 'plugin')
		{
			$folder = empty($folder) ? 'system' : $folder;

			$query->where($db->qn('folder') . ' = ' . $db->q($folder));
		}

		// Module? Use the folder to determine if it's site or admin module.
		if ($type == 'module')
		{
			$folder = empty($folder) ? 'site' : $folder;

			$query->where($db->qn('client_id') . ' = ' . $db->q(($folder == 'site') ? 0 : 1));
		}

		try
		{
			$id = $db->setQuery($query, 0, 1)->loadResult();
		}
		catch (Exception $e)
		{
			$id = 0;
		}

		return empty($id) ? 0 : (int) $id;
	}

	/**
	 * Returns the update site IDs matching the criteria below. All criteria are optional but at least one must be
	 * defined for the method call to make any sense.
	 *
	 * @param   int|null  $includeEID  The update site must belong to this extension ID
	 * @param   int|null  $excludeEID  The update site must NOT belong to this extension ID
	 * @param   array     $locations   The update site must match one of these locations
	 *
	 * @return  array  The IDs of the update sites
	 */
	private function getUpdateSitesFor($includeEID = null, $excludeEID = null, $locations = [])
	{
		$db    = $this->container->db;
		$query = $db->getQuery(true)
			->select($db->qn('s.update_site_id'))
			->from($db->qn('#__update_sites', 's'));

		if (!empty($locations))
		{
			$quotedLocations = array_map([$db, 'q'], $locations);
			$query->where($db->qn('location') . 'IN(' . implode(',', $quotedLocations) . ')');
		}

		if (!empty($includeEID) || !empty($excludeEID))
		{
			$query->innerJoin($db->qn('#__update_sites_extensions', 'e') . 'ON(' . $db->qn('e.update_site_id') .
				' = ' . $db->qn('s.update_site_id') . ')'
			);
		}

		if (!empty($includeEID))
		{
			$query->where($db->qn('e.extension_id') . ' = ' . $db->q($includeEID));
		}
		elseif (!empty($excludeEID))
		{
			$query->where($db->qn('e.extension_id') . ' != ' . $db->q($excludeEID));
		}

		try
		{
			$ret = $db->setQuery($query)->loadColumn();
		}
		catch (Exception $e)
		{
			$ret = null;
		}

		return empty($ret) ? [] : $ret;
	}

	private function createFakePackageExtension()
	{
		$manifestCacheJson = json_encode([
			'name'         => 'Akeeba Backup package',
			'type'         => 'package',
			'creationDate' => gmdate('Y-m-d'),
			'author'       => 'Nicholas K. Dionysopoulos',
			'copyright'    => sprintf('Copyright (c)2006-%d Akeeba Ltd / Nicholas K. Dionysopoulos', gmdate('Y')),
			'authorEmail'  => '',
			'authorUrl'    => 'https://www.akeeba.com',
			'version'      => $this->version,
			'description'  => sprintf('Akeeba Backup installation package v.%s', $this->version),
			'group'        => '',
			'filename'     => 'pkg_akeeba',
		]);

		$extensionRecord = [
			'name'             => 'Akeeba Backup package',
			'type'             => 'package',
			'element'          => 'pkg_akeeba',
			'folder'           => '',
			'client_id'        => 0,
			'enabled'          => 1,
			'access'           => 1,
			'protected'        => 0,
			'manifest_cache'   => $manifestCacheJson,
			'params'           => '{}',
			'checked_out'      => 0,
			'checked_out_time' => null,
			'state'            => 0,
		];

		$class = '\\Joomla\\CMS\\Table\\Extension';
		$class = class_exists($class, true) ? $class : '\\JTableExtension';
		$extension = new $class($this->container->db);
		$extension->save($extensionRecord);

		$this->createFakePackageManifest();
	}

	private function createFakePackageManifest()
	{
		$path = JPATH_ADMINISTRATOR . '/manifests/packages/pkg_akeeba.xml';

		if (file_exists($path))
		{
			return;
		}

		$isPro   = defined('AKEEBA_PRO') ? AKEEBA_PRO : 0;
		$proCore = $isPro ? 'pro' : 'core';
		$dlid    = $isPro ? '<dlid prefix="dlid=" suffix=""/>' : '';
		$year    = gmdate('Y');
		$date    = gmdate('Y-m-d');

		$proPlugins = <<< END
        <file type="file" id="file_akeeba">file_akeeba-pro.zip</file>
		<file type="plugin" group="installer" id="akeebabackup">plg_installer_akeebabackup.zip</file>
END;
		$proPlugins = $isPro ? $proPlugins : '';

		$content = <<< XML
<?xml version="1.0" encoding="utf-8"?>
<extension version="3.9.0" type="package" method="upgrade">
	$dlid
    <name>Akeeba Backup package</name>
    <author>Nicholas K. Dionysopoulos</author>
    <creationDate>$date</creationDate>
    <packagename>akeeba</packagename>
    <version>{$this->version}</version>
    <url>https://www.akeeba.com</url>
    <packager>Akeeba Ltd</packager>
    <packagerurl>https://www.akeeba.com</packagerurl>
    <copyright>Copyright (c)2006-$year Akeeba Ltd / Nicholas K. Dionysopoulos</copyright>
    <license>GNU GPL v3 or later</license>
    <description>Akeeba Backup installation package {$this->version}</description>

    <files>
        <file type="component" id="com_akeebabackup">com_akeebabackup-{$proCore}.zip</file>
        <file type="plugin" group="console" id="akeebabackup">plg_console_akeebabackup.zip</file>
        <file type="plugin" group="quickicon" id="akeebabackup">plg_quickicon_akeebabackup.zip</file>
        <file type="plugin" group="system" id="backuponupdate">plg_system_backuponupdate.zip</file>
        <file type="plugin" group="actionlog" id="akeebabackup">plg_actionlog_akeebabackup.zip</file>
        $proPlugins
    </files>

    <scriptfile>script.akeeba.php</scriptfile>
</extension>
XML;

		if (!@file_put_contents($content, $path))
		{
			File::write($path, $content);
		}
	}
}
