/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.syntaxhighlight = 'csharp';
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Assets/admin/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assets/admin/plugins/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFileBrowseUrl = '/Assets/admin/plugins/ckfinder/ckfinder.html?Type=Files';
    config.filebrowserFlashBrowseUrl = '/Assets/admin/plugins/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Assets/admin/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload?type=Files';
    config.filebrowserImageUploadUrl = '/Data/';
    config.filebrowserFlashUploadUrl = '/Assets/admin/plugins/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFileUploadUrl = '/Assets/admin/plugins/ckfinder/ckfinder.html?Type=Files';
};
