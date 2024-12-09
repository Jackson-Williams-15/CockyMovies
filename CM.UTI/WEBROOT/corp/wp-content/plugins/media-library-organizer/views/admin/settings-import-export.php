<?php
/**
 * Output Import and Export options.
 *
 * @since   1.0.0
 *
 * @package Media_Library_Organizer
 * @author  WP Media Library
 */

if ( ! defined( 'ABSPATH' ) ) {
	exit; // Exit if accessed directly.
}
?>

<div class="postbox wpzinc-vertical-tabbed-ui">
	<!-- Second level tabs -->
	<ul class="wpzinc-nav-tabs wpzinc-js-tabs" data-panels-container="#settings-container" data-panel=".panel" data-active="wpzinc-nav-tab-vertical-active">
		<?php
		// Iterate through this screen's tabs.
		foreach ( (array) $tabs as $tab_item ) {
			?>
			<li class="wpzinc-nav-tab <?php echo esc_attr( ( isset( $tab_item['menu_icon'] ) ? $tab_item['menu_icon'] : 'default' ) ); ?>">
				<a href="#<?php echo esc_attr( $tab_item['name'] ); ?>"<?php echo ( $tab_item['name'] === $tab['name'] ? ' class="wpzinc-nav-tab-vertical-active"' : '' ) . ( isset( $tab_item['documentation'] ) ? ' data-documentation="' . esc_attr( $tab_item['documentation'] ) . '"' : '' ); ?>>
					<?php
					echo esc_html( $tab_item['label'] );
					?>
				</a>
			</li>
			<?php
		}
		?>
	</ul>

	<!-- Content -->
	<div id="settings-container" class="wpzinc-nav-tabs-content no-padding">
		<!-- Import -->
		<div id="import" class="panel">
			<div class="postbox">
				<header>
					<h3><?php esc_html_e( 'Import', 'media-library-organizer' ); ?></h3>
					<p class="description">
						<?php esc_html_e( 'Upload a JSON file generated by this Plugin\'s export functionality.  This will overwrite any existing settings stored on this installation.', 'media-library-organizer' ); ?>
					</p>
				</header>

				<div class="wpzinc-option">
					<div class="left">
						<label for="file"><?php esc_html_e( 'JSON File', 'media-library-organizer' ); ?></label>
					</div>
					<div class="right">
						<input type="file" id="file" name="import" />
					</div>
				</div>

				<div class="wpzinc-option">
					<input name="submit" type="submit" class="button button-primary" value="<?php esc_attr_e( 'Import', 'media-library-organizer' ); ?>" />              
				</div>
			</div>
		</div>

		<?php
		// Iterate through import sources, outputting a view for each.
		if ( count( $screen['data']['import_sources'] ) > 0 ) {
			foreach ( $screen['data']['import_sources'] as $import_source ) {
				?>
				<div id="import-<?php echo esc_attr( $import_source['name'] ); ?>" class="panel">
					<div class="postbox">
						<header>
							<h3>
								<?php
								/* translators: Import Source Name */
								printf( esc_html__( 'Import from %s', 'media-library-organizer' ), esc_html( $import_source['label'] ) );
								?>
							</h3>
							<p class="description">
								<?php
								/* translators: Import Source Name */
								printf( esc_html__( 'Imports %s data found on this WordPress installation. This will overwrite any existing settings in this Plugin.', 'media-library-organizer' ), esc_html( $import_source['label'] ) );
								?>
							</p>
						</header>

						<?php
						// Output any fields / options that this Import Source might have specified.
						do_action( 'media_library_organizer_import_view_' . $import_source['name'] );
						?>

						<div class="wpzinc-option">
							<input name="import_<?php echo esc_attr( $import_source['name'] ); ?>" type="submit" class="button button-primary" value="<?php esc_attr_e( 'Import', 'media-library-organizer' ); ?>" />
						</div>
					</div>
				</div>
				<?php
			}
		}
		?>

		<!-- Export -->
		<div id="export" class="panel">
			<div class="postbox">
				<header>
					<h3><?php esc_html_e( 'Export', 'media-library-organizer' ); ?></h3>
					<p class="description">
						<?php esc_html_e( 'To export this Plugin\'s settings, click the Export button below.', 'media-library-organizer' ); ?>
						<br ?>
						<?php esc_html_e( 'You can then import the generated JSON file into another Plugin installation.', 'media-library-organizer' ); ?>
					</p>
				</header>

				<div class="wpzinc-option">
					<a href="admin.php?page=<?php echo esc_attr( $this->base->plugin->name ); ?>-import-export&export=1" class="button button-primary" title="<?php esc_attr_e( 'Export', 'media-library-organizer' ); ?>">
						<?php esc_html_e( 'Export', 'media-library-organizer' ); ?>
					</a>
				</div>
			</div>
		</div>
	</div>
</div>
