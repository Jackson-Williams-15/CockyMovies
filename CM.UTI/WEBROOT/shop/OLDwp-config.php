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
define( 'DB_NAME', 'COCKYSHOP' );

/** Database username */
define( 'DB_USER', 'root' );

/** Database password */
define( 'DB_PASSWORD', '*Columbia1' );

/** Database hostname */
define( 'DB_HOST', 'localhost:3307' );

/** Database charset to use in creating database tables. */
define( 'DB_CHARSET', 'utf8mb4' );

/** The database collate type. Don't change this if in doubt. */
define( 'DB_COLLATE', '' );

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
define( 'AUTH_KEY',         '/a!kC$w=w7S?qPB&Q7<pFQZWp1c,T]!8i#fqR%NSWu3aom7?WHXo?[gy9[A3;Ni&' );
define( 'SECURE_AUTH_KEY',  'Ytv SHBZ6:h#9nn=YTQt 8qc$Ostqup`D5n8 !j)$2+U2 1udasZ`AL_b)Bb.|F@' );
define( 'LOGGED_IN_KEY',    'i{mr+(&].zO&De. (B8bRLqoq-Zw_ztD$)XjHjbP-8vNya}9z)$id1?xT0] OmA(' );
define( 'NONCE_KEY',        'zdxyy4qoyEHxoXTBi!6mV`YC%=:R4K~I6v_X2eQY^({y*40:.LWK`*1qP[41}{x,' );
define( 'AUTH_SALT',        '0H8dgjiZ}*T|*PJp{LN&J}yE&aP#I/~qu+bmC&~)/EQ0WvQr{-QDG#7I4NvTrc5p' );
define( 'SECURE_AUTH_SALT', 'o`$^r662E:9gFe~ywO4N *;0h/imptu:r7(_t yt)v#:[@O0MwQCrEH_)f@vFCU8' );
define( 'LOGGED_IN_SALT',   '`zs-HM=ZkKn`k>]JCI]t(QD {x<DtPa`}cz<;ZW{~%oJyPvXC@5Ak[L{5x3?F_TU' );
define( 'NONCE_SALT',       '-%!GRI8z8HIS5P)u)LDSQ1d?C|2AxczR:dsQ5PBjfeq&]8br{w`SPMQUcj^}Eh4B' );

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
$table_prefix = 'shop_';

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

