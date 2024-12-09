<?php
/**
 * The base configuration for WordPress
 *
 * The wp-config.php creation script uses this file during the installation.
 * You don't have to use the website, you can copy this file to "wp-config.php"
 * and fill in the values.
 *
 * This file contains the following configurations:
 *
 * * Database settings
 * * Secret keys
 * * Database table prefix
 * * ABSPATH
 *
 * @link https://developer.wordpress.org/advanced-administration/wordpress/wp-config/
 *
 * @package WordPress
 */

// ** Database settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define( 'DB_NAME', 'cockysso' );

/** Database username */
define( 'DB_USER', 'root' );

/** Database password */
define( 'DB_PASSWORD', 'test12345' );

/** Database hostname */
define( 'DB_HOST', 'localhost:3307' );

/** Database charset to use in creating database tables. */
define( 'DB_CHARSET', 'utf8mb4' );

/** The database collate type. Don't change this if in doubt. */
define( 'DB_COLLATE', '' );
define('WP_ALLOW_REPAIR', true);
/**#@+
 * Authentication unique keys and salts.
 *
 * Change these to different unique phrases! You can generate these using
 * the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}.
 *
 * You can change these at any point in time to invalidate all existing cookies.
 * This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define( 'AUTH_KEY',         '!Y*!s];2Emrj6>,xF!qajxGS^E4!(%J41.GHsp-3tP1L& FL~s3#Up;7ppEyl3G`' );
define( 'SECURE_AUTH_KEY',  'Z5Z; uu=$!0~nUafB/C*RF:N,QxVzJ.t_c#p`{g7,(B_JJB3JH>EtBSuWeobbw3n' );
define( 'LOGGED_IN_KEY',    'Js`AyAo.bG- N),=DEMyt:$4w4taW;?mp{Tk+T6Dh,C9iN]1jtf.)>,G .Lhx:Wk' );
define( 'NONCE_KEY',        'Z$th.ze<9gpcM9;o=$)<C_>h34<{c^[Ho&3qz`bPBdgYn`eW*mn6pXH^GVWo~0ih' );
define( 'AUTH_SALT',        'z+YpN5(P1LAQ7DJDN{%4.oT}t`:rFwLJwzV@FZ@):y(#g^9RPt+D0zX}5EIPa}`u' );
define( 'SECURE_AUTH_SALT', 'UOE7f6*q: =!o)huL4KQ}Go${=tUdGXY&SI%&Sz~{Bthk<@7YQ!-/:hk1MRTNEr2' );
define( 'LOGGED_IN_SALT',   '`]A>~[tEI4-A2$j.s{,0Wl0Bo[XzQQ!;8VlDKtP?Up00uneG~$Oz:H*Q._s5&O8k' );
define( 'NONCE_SALT',       'Ox )uo47.qAOj(ue;sSZE|Hv&%w;Ph5T 6HM(IoY)0n,RN* 7KEVr5@z36U=J{e/' );

/**#@-*/

/**
 * WordPress database table prefix.
 *
 * You can have multiple installations in one database if you give each
 * a unique prefix. Only numbers, letters, and underscores please!
 *
 * At the installation time, database tables are created with the specified prefix.
 * Changing this value after WordPress is installed will make your site think
 * it has not been installed.
 *
 * @link https://developer.wordpress.org/advanced-administration/wordpress/wp-config/#table-prefix
 */
$table_prefix = 'corp_';

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 *
 * For information on other constants that can be used for debugging,
 * visit the documentation.
 *
 * @link https://developer.wordpress.org/advanced-administration/debug/debug-wordpress/
 */
define( 'WP_DEBUG', false );

/* Add any custom values between this line and the "stop editing" line. */



/* That's all, stop editing! Happy publishing. */

/** Absolute path to the WordPress directory. */
if ( ! defined( 'ABSPATH' ) ) {
	define( 'ABSPATH', __DIR__ . '/' );
}

/** Sets up WordPress vars and included files. */
require_once ABSPATH . 'wp-settings.php';
define('FS_METHOD', 'direct');