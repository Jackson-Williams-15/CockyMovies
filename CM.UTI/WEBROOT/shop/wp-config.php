<?php
define( 'WP_CACHE', true );
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
define( 'DB_USER', 'cockysa' );

/** Database password */
define( 'DB_PASSWORD', 'test12345' );

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
define( 'AUTH_KEY',         'J(+*WUu)i`;3>^ZC2Tbv6%N``3q]3q+$$D{meRp d;x7ls=C9%JWBe6E9ey[?9F2' );
define( 'SECURE_AUTH_KEY',  'T$l]l*>x=#2Ty8K(3<Tj=-2{f:xz4riOBp*&w}g0P+Y?5Sfs#dc#7qZXd>jh{5OA' );
define( 'LOGGED_IN_KEY',    'qs0b%,LqlY=cN_/<VNE1tvFIe1EaVzb=pBiu@SrWvjN.#!=T*Y jXRtIkBNPeA`N' );
define( 'NONCE_KEY',        '%:&EJS0$bxdWN<uz-zUr!$#MN|f$7n0*;g61X5^l)?!M7Fzot[?* jk^F-nqhnLI' );
define( 'AUTH_SALT',        '^aT^u3j/} {4UY=6 0%z>^fQ=CV>97U{RR!QYtN6_`dFwoL6c/Wm~xY*Y$(:1h9@' );
define( 'SECURE_AUTH_SALT', 'P~chd]w1A.<{w^Z}-O;2Q@3FiKt9IW,7V9Ru<i4GB!n;|rvb-*x&H)8 `g(eCM92' );
define( 'LOGGED_IN_SALT',   '(oS%o8FBF>x)gE#[E tYRMans.2M5%r5$f}k p,f8t];(+z-5dz sw$ZX{APqH6 ' );
define( 'NONCE_SALT',       '%X)Wr_XsDgJ88B|>hYo6)$^OeUs`oE5SXt:GAC^)CQJp?>t]7y+dlxcX*BxG9~^P' );

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
$table_prefix = 'shop2_';

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
