function mediaLibraryOrganizerTreeViewContextMenuInit(){var $;($=jQuery)("#media-library-organizer-tree-view-list").contextmenu({delegate:".cat-item",menu:media_library_organizer_tree_view.context_menu,select:function(e,r){var i=mediaLibraryOrganizerTreeViewGetTermIDFromElement(r.target.parent()),a=mediaLibraryOrganizerTreeViewGetTermNameFromElement(r.target);switch(r.cmd){case"create_term":mediaLibraryOrganizerTreeViewAddCategory(i);break;case"edit_term":mediaLibraryOrganizerTreeViewEditCategory(i,a);break;case"delete_term":mediaLibraryOrganizerTreeViewDeleteCategory(i,a);break;default:let e={term_id:i,term_name:a};wp.media.events.trigger("mlo:grid:tree-view:context-menu:"+r.cmd,{media_library_organizer_tree_view:media_library_organizer_tree_view,atts:e});break}}})}function mediaLibraryOrganizerTreeViewAddCategory(e){!function($){var r=prompt(media_library_organizer_tree_view.actions.create_term.prompt);if(r&&r.length){var i={action:media_library_organizer_tree_view.actions.create_term.action,nonce:media_library_organizer_tree_view.actions.create_term.nonce,taxonomy_name:media_library_organizer_tree_view.taxonomy.name,term_name:r,term_parent_id:e};i[media_library_organizer_tree_view.taxonomy.name]=media_library_organizer_tree_view.selected_term,$.post(media_library_organizer_tree_view.ajaxurl,i,(function(e){if(e.success){var r=e.data;r.selected_term=media_library_organizer_tree_view.selected_term,r.media_view=media_library_organizer_tree_view.media_view,wp.media.events.trigger("mlo:grid:tree-view:added:term",r),mediaLibraryOrganizerTreeViewGet(r.taxonomy.name,r.selected_term)}else alert(e.data)}))}}(jQuery)}function mediaLibraryOrganizerTreeViewEditCategory(e,r){!function($){if(e){var i=prompt(media_library_organizer_tree_view.actions.edit_term.prompt,r);if(i&&i.length){var a={action:media_library_organizer_tree_view.actions.edit_term.action,nonce:media_library_organizer_tree_view.actions.edit_term.nonce,taxonomy_name:media_library_organizer_tree_view.taxonomy.name,term_id:e,term_name:i};a[media_library_organizer_tree_view.taxonomy.name]=media_library_organizer_tree_view.selected_term,$.post(media_library_organizer_tree_view.ajaxurl,a,(function(e){if(e.success){var r=e.data;r.selected_term=media_library_organizer_tree_view.selected_term,r.media_view=media_library_organizer_tree_view.media_view,wp.media.events.trigger("mlo:grid:tree-view:edited:term",r),mediaLibraryOrganizerTreeViewGet(r.taxonomy.name,r.selected_term)}else alert(e.data)}))}}else alert(media_library_organizer_tree_view.actions.edit_term.no_selection)}(jQuery)}function mediaLibraryOrganizerTreeViewDeleteCategory(e,r){!function($){var i;if(e){if(confirm(media_library_organizer_tree_view.actions.delete_term.prompt+" "+r)){var a={action:media_library_organizer_tree_view.actions.delete_term.action,nonce:media_library_organizer_tree_view.actions.delete_term.nonce,taxonomy_name:media_library_organizer_tree_view.taxonomy.name,term_id:e};a[media_library_organizer_tree_view.taxonomy.name]=media_library_organizer_tree_view.selected_term,$.post(media_library_organizer_tree_view.ajaxurl,a,(function(e){if(e.success){var r=e.data;r.selected_term=media_library_organizer_tree_view.selected_term,r.media_view=media_library_organizer_tree_view.media_view,wp.media.events.trigger("mlo:grid:tree-view:deleted:term",r),mediaLibraryOrganizerTreeViewGet(r.taxonomy.name,r.selected_term)}else alert(e.data)}))}}else alert(media_library_organizer_tree_view.actions.delete_term.no_selection)}(jQuery)}function mediaLibraryOrganizerTreeViewAssignAttachmentsToCategory(e,r){var $;$=jQuery,e&&r&&$.post(media_library_organizer_tree_view.ajaxurl,{action:media_library_organizer_tree_view.actions.categorize_attachments.action,nonce:media_library_organizer_tree_view.actions.categorize_attachments.nonce,taxonomy_name:media_library_organizer_tree_view.taxonomy.name,attachment_ids:e,term_id:r},(function(e){if(e.success){wpzinc_notification_show_success_message(media_library_organizer_tree_view.labels.categorized_attachments.replace("%s",e.data.attachments.length));var r=e.data;r.selected_term=media_library_organizer_tree_view.selected_term,r.media_view=media_library_organizer_tree_view.media_view,wp.media.events.trigger("mlo:grid:tree-view:assigned:attachments:term",r)}else wpzinc_notification_show_error_message(e.data)}))}function mediaLibraryOrganizerTreeViewContextualButtons(){var $;($=jQuery)("#media-library-organizer-tree-view-list .current-cat").length?($("button.media-library-organizer-tree-view-edit").prop("disabled",!1),$("button.media-library-organizer-tree-view-delete").prop("disabled",!1)):($("button.media-library-organizer-tree-view-edit").prop("disabled",!0),$("button.media-library-organizer-tree-view-delete").prop("disabled",!0))}function mediaLibraryOrganizerTreeViewGet(e,r){var $;($=jQuery).post(media_library_organizer_tree_view.ajaxurl,{action:media_library_organizer_tree_view.actions.get_tree_view.action,nonce:media_library_organizer_tree_view.actions.get_tree_view.nonce,taxonomy_name:e,current_term:r},(function(e){if(!e.success)return!1;mediaLibraryOrganizerTreeViewDestroyJsTree(),$("#media-library-organizer-tree-view-list").html(e.data),mediaLibraryOrganizerTreeViewInitJsTree(),mediaLibraryOrganizerTreeViewContextualButtons(),mediaLibraryOrganizerTreeViewInitDroppable(),wp.media.events.trigger("mlo:grid:tree-view:loaded")}))}function mediaLibraryOrganizerTreeViewInitJsTree(){var $;($=jQuery)(".media-library-organizer-tree-view-enabled").length&&($("li.current-cat-ancestor",$(".media-library-organizer-tree-view-enabled")).each((function(){$(this).addClass("jstree-open")})),$(".media-library-organizer-tree-view-enabled").jstree().bind("select_node.jstree",(function(e,r){document.location.href=r.node.a_attr.href})).bind("open_node.jstree",(function(e,r){mediaLibraryOrganizerTreeViewInitDroppable()})))}function mediaLibraryOrganizerTreeViewDestroyJsTree(){var $;($=jQuery)(".media-library-organizer-tree-view-enabled").length&&$(".media-library-organizer-tree-view-enabled").jstree("destroy")}function mediaLibraryOrganizerTreeViewListInitDraggable(){var $;($=jQuery)("td.title.column-title strong.has-media-icon, td.tree-view-move span.dashicons-move").draggable({appendTo:"body",revert:!0,cursorAt:{top:10,left:10},helper:function(){var e=$(this).closest("tr").attr("id").split("-")[1],r=[e];$("table.media tbody input:checked").length>0&&$("table.media tbody input:checked").each((function(){$(this).val()!=e&&r.push($(this).val())}));var i="";return i=r.length>1?media_library_organizer_tree_view.labels.categorize_attachments.replace("%s",r.length):media_library_organizer_tree_view.labels.categorize_attachment,$('<div id="media-library-organizer-tree-view-draggable" data-attachment-ids="'+r.join(",")+'">'+i+"</div>")}})}function mediaLibraryOrganizerTreeViewGridInitDraggable(){var $;($=jQuery)("li.attachment").draggable({appendTo:"body",revert:!0,cursorAt:{top:40,left:10},helper:function(){var e=$(this).data("id"),r=[e];if(mediaLibraryOrganizerTreeViewGridSelectedAttachments.length>0)for(var i=mediaLibraryOrganizerTreeViewGridSelectedAttachments.length,a=0;a<i;a++)mediaLibraryOrganizerTreeViewGridSelectedAttachments.models[a].id!=e&&r.push(mediaLibraryOrganizerTreeViewGridSelectedAttachments.models[a].id);var t="";return t=r.length>1?media_library_organizer_tree_view.labels.categorize_attachments.replace("%s",r.length):media_library_organizer_tree_view.labels.categorize_attachment,$('<div id="media-library-organizer-tree-view-draggable" data-attachment-ids="'+r.join(",")+'">'+t+"</div>")}})}function mediaLibraryOrganizerTreeViewInitDroppable(){var $;($=jQuery)("#media-library-organizer-tree-view-list li.cat-item a, #media-library-organizer-tree-view-list li.cat-item-unassigned a").droppable({hoverClass:"media-library-organizer-tree-view-droppable-hover",drop:function(e,r){var i=$(r.helper).data("attachment-ids"),a;i.toString().search(",")&&(i=i.toString().split(",")),mediaLibraryOrganizerTreeViewAssignAttachmentsToCategory(i,mediaLibraryOrganizerTreeViewGetTermIDFromElement($(e.target).parent()))}})}function mediaLibraryOrganizerTreeViewGetTermIDFromElement(e){if(void 0===e[0])return!1;if(void 0===e[0].className)return!1;for(var r=e[0].className.split(" "),i=r.length,a=0;a<i;a++)if(-1!=r[a].search("cat-item-")){var t=r[a].replace("cat-item-","");return"unassigned"==t?-1:t}return!1}function mediaLibraryOrganizerTreeViewGetTermNameFromElement(e){return jQuery(e).contents().filter((function(){return 3==this.nodeType}))[0].nodeValue.trim()}var mediaLibraryOrganizerTreeViewGridSelectedAttachments,mediaLibraryOrganizerTreeViewGridModified;"grid"==media_library_organizer_tree_view.media_view&&(jQuery(document).ready((function($){var e;new MutationObserver(mediaLibraryOrganizerTreeViewGridInitDraggable).observe(document.querySelector(".attachments-browser ul.attachments"),{childList:!0})})),function($,e){e.extend(wp.media.view.AttachmentFilters.prototype,{select:function(){mediaLibraryOrganizerTreeViewGridSelectedAttachments=this.controller.state().get("selection")}}),e.extend(wp.media.controller.Library.prototype,{refreshContent:function(){mediaLibraryOrganizerTreeViewGridSelectedAttachments=this.get("selection")}})}(jQuery,_)),jQuery(document).ready((function($){if($("body").hasClass("upload-php")){$(".wrap").wrap('<div class="media-library-organizer-tree-view"></div>'),$(".media-library-organizer-tree-view").prepend($("#media-library-organizer-tree-view")),$("#media-library-organizer-tree-view").show();var e=new StickySidebar("#media-library-organizer-tree-view",{containerSelector:".media-library-organizer-tree-view",innerWrapperSelector:".media-library-organizer-tree-view-inner"});0!=media_library_organizer_tree_view.context_menu&&mediaLibraryOrganizerTreeViewContextMenuInit(),mediaLibraryOrganizerTreeViewInitJsTree(),mediaLibraryOrganizerTreeViewListInitDraggable(),mediaLibraryOrganizerTreeViewInitDroppable(),mediaLibraryOrganizerTreeViewContextualButtons(),$("body").on("click",".media-library-organizer-tree-view-add",(function(e){var r;e.preventDefault(),mediaLibraryOrganizerTreeViewAddCategory(mediaLibraryOrganizerTreeViewGetTermIDFromElement($("#media-library-organizer-tree-view-list .current-cat")))})),$("body").on("click",".media-library-organizer-tree-view-edit",(function(e){var r,i;e.preventDefault(),mediaLibraryOrganizerTreeViewEditCategory(mediaLibraryOrganizerTreeViewGetTermIDFromElement($("#media-library-organizer-tree-view-list .current-cat")),mediaLibraryOrganizerTreeViewGetTermNameFromElement($("#media-library-organizer-tree-view-list .current-cat a")))})),$("body").on("click",".media-library-organizer-tree-view-delete",(function(e){var r,i;e.preventDefault(),mediaLibraryOrganizerTreeViewDeleteCategory(mediaLibraryOrganizerTreeViewGetTermIDFromElement($("#media-library-organizer-tree-view-list .current-cat")),mediaLibraryOrganizerTreeViewGetTermNameFromElement($("#media-library-organizer-tree-view-list .current-cat a")))}))}})),wp.media.events.on("mlo:grid:tree-view:added:term",(function(e){!function($){switch(e.media_view){case"list":mediaLibraryOrganizerListViewReplaceTaxonomyFilter(e.taxonomy.name,e.dropdown_filter,e.selected_term);break;case"grid":MediaLibraryOrganizerAttachmentsBrowser.controller.isModeActive("select")||mediaLibraryOrganizerGridViewReplaceTaxonomyFilter(e.taxonomy.name,e.terms,e.taxonomy.labels.all_items,media_library_organizer_media.labels.unassigned);break}}(jQuery)})),wp.media.events.on("mlo:grid:tree-view:edited:term",(function(e){!function($){switch(e.media_view){case"list":mediaLibraryOrganizerListViewReplaceTaxonomyFilter(e.taxonomy.name,e.dropdown_filter,e.selected_term),mediaLibraryOrganizerListViewUpdateAttachmentTerms(e.taxonomy.name,e.old_term,e.term);break;case"grid":MediaLibraryOrganizerAttachmentsBrowser.controller.isModeActive("select")||mediaLibraryOrganizerGridViewReplaceTaxonomyFilter(e.taxonomy.name,e.terms,e.taxonomy.labels.all_items,media_library_organizer_media.labels.unassigned),void 0!==wp.media.frame.library?wp.media.frame.library.props.set({ignore:+new Date}):(wp.media.frame.content.get().collection.props.set({ignore:+new Date}),wp.media.frame.content.get().options.selection.reset());break}}(jQuery)})),wp.media.events.on("mlo:grid:tree-view:deleted:term",(function(e){!function($){switch(e.media_view){case"list":if(e.selected_term==e.term.slug)return void(window.location.href="upload.php?mode=list");mediaLibraryOrganizerListViewReplaceTaxonomyFilter(e.taxonomy.name,e.dropdown_filter,e.selected_term),mediaLibraryOrganizerListViewUpdateAttachmentTerms(e.taxonomy.name,e.term,!1);break;case"grid":if(e.selected_term==e.term.slug)return void(window.location.href="upload.php?mode=grid");MediaLibraryOrganizerAttachmentsBrowser.controller.isModeActive("select")||mediaLibraryOrganizerGridViewReplaceTaxonomyFilter(e.taxonomy.name,e.terms,e.taxonomy.labels.all_items,media_library_organizer_media.labels.unassigned),void 0!==wp.media.frame.library?wp.media.frame.library.props.set({ignore:+new Date}):(wp.media.frame.content.get().collection.props.set({ignore:+new Date}),wp.media.frame.content.get().options.selection.reset());break}}(jQuery)})),wp.media.events.on("mlo:grid:tree-view:assigned:attachments:term",(function(e){!function($){switch(e.media_view){case"list":mediaLibraryOrganizerListViewReplaceTaxonomyFilter(e.taxonomy.name,e.dropdown_filter,media_library_organizer_tree_view.selected_term);for(let a in e.attachments){var r=[],i=e.attachments[a].terms.length;for(j=0;j<i;j++)r.push('<a href="upload.php?taxonomy='+e.attachments[a].terms[j].taxonomy+"&term="+e.attachments[a].terms[j].slug+'">'+e.attachments[a].terms[j].name+"</a>");$("tr#post-"+e.attachments[a].id+" td.taxonomy-"+e.taxonomy.name).html(r.join(", "))}break;case"grid":MediaLibraryOrganizerAttachmentsBrowser.controller.isModeActive("select")&&MediaLibraryOrganizerAttachmentsBrowser.controller.deactivateMode("select").activateMode("edit"),mediaLibraryOrganizerGridViewReplaceTaxonomyFilter(e.taxonomy.name,e.terms,e.taxonomy.labels.all_items,media_library_organizer_media.labels.unassigned),mediaLibraryOrganizerGridViewRefresh();break}e.taxonomy.name==media_library_organizer_tree_view.taxonomy.name&&mediaLibraryOrganizerTreeViewGet(e.taxonomy.name,e.selected_term)}(jQuery)})),wp.media.events.on("mlo:grid:edit-attachment:added:term",(function(e){e.taxonomy.name==media_library_organizer_tree_view.taxonomy.name&&mediaLibraryOrganizerTreeViewGet(e.taxonomy.name,media_library_organizer_tree_view.selected_term)})),wp.media.events.on("mlo:grid:edit-attachment:edited",(function(e){var $;$=jQuery,e.taxonomy_term_changed&&mediaLibraryOrganizerTreeViewGet(media_library_organizer_tree_view.taxonomy.name,media_library_organizer_tree_view.selected_term)})),wp.media.events.on("mlo:grid:filter:change:term",(function(e){e.taxonomy_name==media_library_organizer_tree_view.taxonomy.name&&(media_library_organizer_tree_view.selected_term=e.slug,mediaLibraryOrganizerTreeViewGet(e.taxonomy_name,e.slug))})),wp.media.events.on("mlo:grid:attachment:upload:success",(function(e){mediaLibraryOrganizerTreeViewGet(media_library_organizer_tree_view.taxonomy.name,media_library_organizer_tree_view.selected_term)})),wp.media.events.on("mlo:grid:edit-attachment:deleted",(function(e){mediaLibraryOrganizerTreeViewGet(media_library_organizer_tree_view.taxonomy.name,media_library_organizer_tree_view.selected_term)})),wp.media.events.on("mlo:grid:attachments:bulk_actions:done",(function(){mediaLibraryOrganizerTreeViewGet(media_library_organizer_tree_view.taxonomy.name,media_library_organizer_tree_view.selected_term)}));