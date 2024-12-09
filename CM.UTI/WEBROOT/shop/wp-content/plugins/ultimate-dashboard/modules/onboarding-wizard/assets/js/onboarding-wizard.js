(()=>{function e(e){let n=document.querySelector(e);return n instanceof HTMLElement?n:null}function n(e){let n=document.querySelectorAll(e),t=[];for(let e=0;e<n.length;e++){let s=n[e];s instanceof HTMLElement&&t.push(s)}return t}function t(e){let n=document.querySelector(e);return n instanceof HTMLInputElement?n:null}function s(e){let n=document.querySelectorAll(e),t=[];for(let e=0;e<n.length;e++){let s=n[e];s instanceof HTMLInputElement&&t.push(s)}return t}!function(i){let o;let a=e(".udb-onboarding-wizard-page"),d=e(".onboarding-wizard-heatbox .heatbox-footer"),r=e(".onboarding-wizard-heatbox .skip-button"),c=e(".onboarding-wizard-heatbox .save-button"),l=e("#skip-setup-onboarding-wizard"),u=e(".onboarding-wizard-heatbox .subscribe-button"),g=e(".udb-onboarding-wizard-skip-discount a"),b=n("[data-udb-show-on='subscribe']"),f=n("[data-udb-show-on='skip-discount']"),h=e(".onboarding-wizard-heatbox .for-discount"),v=t("#udb_modules__login_redirect"),m=e("#onboarding-wizard-explore-settings"),_=e(".onboarding-wizard-heatbox .udb-dots"),p=e("#remove_by_roles"),w=window.udbOnboardingWizard,L=["modules","widgets","general_settings","custom_login_url","subscription","finished"],k="modules",x=!1,E=[];function z(){return v?.checked??!1}function y(e){var n=[];e.length&&e.forEach(function(e){n.push(e.id)}),E=n}function j(t){(function(e){switch(e){case"modules":h?.classList.add("is-hidden"),d?.classList.remove("is-hidden"),T("Next",["js-save-widgets","js-save-general-settings","js-save-custom-login-url"]);break;case"widgets":T("Next",["js-save-general-settings","js-save-custom-login-url"],"js-save-widgets");break;case"general_settings":T("Next",["js-save-widgets","js-save-custom-login-url"],"js-save-general-settings");break;case"custom_login_url":T("Next",["js-save-widgets","js-save-general-settings"],"js-save-custom-login-url");break;case"subscription":h?.classList.remove("is-hidden"),d?.classList.add("is-hidden");break;case"finished":h?.classList.add("is-hidden"),d?.classList.add("is-hidden")}})(k=L[t.index]),function(e){let t=n(".tns-nav > button");Array.from(t).slice(0,e).forEach(e=>e.classList.add("completed")),t.forEach((n,t)=>{t>=e&&n.classList.remove("completed")})}(t.index),function(){let n=e(".skip-onboarding-wizard");["subscription","finished"].includes(k)?n?.classList.add("is-hidden"):n?.classList.remove("is-hidden")}(),"finished"===k&&m?.classList.remove("is-hidden")}function T(e,n,t=""){c&&(n.forEach(e=>c.classList.remove(e)),t&&c.classList.add(t),c.textContent=e)}function S(){let n=e(".onboarding-wizard-heatbox .udb-dots .tns-nav");if(!n)return;let t=n.children;t.length<4||(z()?t[3].classList.remove("is-hidden"):t[3].classList.add("is-hidden"))}function C(e,n,t){x=!0,i.post(w?.ajaxUrl??"",e,function(e){x=!1,N(t),e.success?n():alert(e.data)}).fail(H).always(function(){N(t)})}function H(e){var n="Something went wrong";e.responseJSON&&e.responseJSON.data&&(n=e.responseJSON.data),alert(n)}function M(e){e?.classList.add("is-loading")}function N(e){e?.classList.remove("is-loading")}function A(){I(b),I(h,!1),I(f,!1),O(),o.goTo("next")}function I(e,n=!0){e&&(Array.isArray(e)?e.forEach(e=>e.classList.toggle("is-hidden",!n)):e.classList.toggle("is-hidden",!n))}function q(){I(b,!1),I(h,!1),I(f),O(),o.goTo("next")}function O(){let e=n("#menu-posts-udb_widgets .wp-submenu > li > a");if(e.length)for(let n=0;n<e.length;n++){let t=e[n];if(t instanceof HTMLAnchorElement&&t.href.includes("page=udb_onboarding_wizard")){t.parentElement?.remove();break}}}a&&d&&r&&c&&h&&u&&g&&(o=window.tns({container:".udb-onboarding-wizard-slides",items:1,loop:!1,autoHeight:!0,controls:!1,navPosition:"bottom",onInit:function(e){o.events.on("indexChanged",j),_&&e.navContainer&&(_.appendChild(e.navContainer),function(e){if(_&&e.navContainer){let n=e.navContainer.children;if(n.length>2){let e=n[n.length-1],t=n[n.length-2];e instanceof HTMLElement&&(e.style.display="none"),t instanceof HTMLElement&&(t.style.display="none")}}else console.error("dotsWrapper is not defined.")}(e)),setTimeout(()=>{S()},1)}}),p&&(i(p).select2(),y(i(p).select2("data")),i(p).on("select2:select",function(e){let n=i(p).select2("data");var t=[];"all"===e.params.data.id?(i(p).val("all"),i(p).trigger("change")):n.length&&(n.forEach(function(e){"all"!==e.id&&t.push(e.id)}),i(p).val(t),i(p).trigger("change")),y(i(p).select2("data"))}),i(p).on("select2:unselect",function(e){y(i(p).select2("data"))})),r?.addEventListener("click",function(){switch(k){case"modules":!function(){if(_){let e=_.children;z()?e[3]?.classList.remove("is-hidden"):e[3]?.classList.add("is-hidden")}o.goTo("next")}();break;case"widgets":case"general_settings":case"custom_login_url":o.goTo("next");break;case"subscription":window.location.href=w?.adminUrl??""}}),l?.addEventListener("click",function(){o.goTo(4)}),c?.addEventListener("click",function(){if(!x){let e;M(c),C((e={action:"udb_onboarding_wizard_save_modules",nonce:w?.nonces.saveModules,modules:function(){let e=s('.udb-modules-slide .module-toggle input[type="checkbox"]');if(!e.length)return[];let n=[];return e.forEach(function(e){var t=e.id.replace("udb_modules__","");e.checked&&n.push(t)}),n}()},c?.classList.contains("js-save-widgets")?e={action:"udb_onboarding_wizard_save_widgets",nonce:w?.nonces.saveWidgets,widgets:function(){let e=s('.udb-widgets-slide .widget-toggle input[type="checkbox"]');if(!e.length)return[];let n=[];return e.forEach(function(e){var t=e.id.replace("udb_widgets__","");e.checked&&n.push(t)}),n}()}:c?.classList.contains("js-save-general-settings")?e={action:"udb_onboarding_wizard_save_general_settings",nonce:w?.nonces.saveGeneralSettings,settings:function(){var e=s('.udb-general-settings-slide .setting-toggle input[type="checkbox"]');if(!e.length)return[];let n=[];return e.forEach(function(e){var t=e.id.replace("udb_settings__","");e.checked&&n.push(t)}),n}(),selected_roles:E}:c?.classList.contains("js-save-custom-login-url")&&(e={action:"udb_onboarding_wizard_save_custom_login_url",nonce:w?.nonces.saveCustomLoginUrl,custom_login_url:function(){let e=t("#udb_login_redirect");return e?.value??""}()}),e),()=>{let e;return e="next",void(3===o.getInfo().displayIndex&&(e=z()?e:4),o.goTo(e))},c)}}),u?.addEventListener("click",function(){if(x)return;M(u);let e=t("#udb-subscription-name")?.value??"",n=t("#udb-subscription-email")?.value??"";C({action:"udb_onboarding_wizard_subscribe",nonce:w?.nonces.subscribe,name:e,email:n},A,u)}),g?.addEventListener("click",function(e){e.preventDefault(),x||(M(g?.parentElement),C({action:"udb_onboarding_wizard_skip_discount",nonce:w?.nonces.skipDiscount},q,g?.parentElement))}),v?.addEventListener("change",function(){S()}))}(jQuery)})();
//# sourceMappingURL=onboarding-wizard.js.map