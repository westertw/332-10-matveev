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
define( 'DB_NAME', 'matveev' );

/** Database username */
define( 'DB_USER', 'root' );

/** Database password */
define( 'DB_PASSWORD', '' );

/** Database hostname */
define( 'DB_HOST', 'localhost' );

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
define( 'AUTH_KEY',         '.H3_rG}{s%g0.e&)Msu_1Io=hSCN)Ja!p,^Y=Dw(i0WLRs/8;gKMA?wT`.K&RI U' );
define( 'SECURE_AUTH_KEY',  'Tn) %W:EsfzJ0Ply4K8?:KTxRBve}ka_0tN(0D=Qtd?{q;c4|?C01X|AWXoV4yI=' );
define( 'LOGGED_IN_KEY',    '.Z_1mX<=t6]qpSpxzD2C2giG3fv Uhp}15]Ia.E?0@l{ne[h#&=Vg9L*qM+T^Zh}' );
define( 'NONCE_KEY',        'aeg3Kxz<>9pPXr%ZdE3E`{nrW()kR|0Ct8@Co#G<h%I1j6KvAy(TK@`9Fiv0V_7&' );
define( 'AUTH_SALT',        'UOzKOKyt.Y)O=8>{O[qW=>S*mh7Q9aP(y7FQ>.!aVS`,(A&TK`8%hUKJiyaeKOSB' );
define( 'SECURE_AUTH_SALT', 'H]@c%D<RD|XsZ:c7]}](i=f7H_lo&_2=FspxMQ`Lo36S~x|[yC;2p,,H?P{*&Y$h' );
define( 'LOGGED_IN_SALT',   'UEa_*RDlD(U]WGUHn}K1V+j|=KC50RA2[XDif[AEF%/D!~iuHea@o=6f.Bn*Io<%' );
define( 'NONCE_SALT',       '~gAO0<I _BbD9qj$Q.C]Z846r:oxIkD/i7L1s4S!U3T,Xr0Ra`]`.hXINL/*-fB=' );

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
$table_prefix = 'wp_';

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
