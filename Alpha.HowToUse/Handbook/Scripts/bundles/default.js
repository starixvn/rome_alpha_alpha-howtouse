var AutoSuggestion = /** @class */ (function () {
    function AutoSuggestion($) {
        var _this = this;
        this.minSearchLength = 2;
        this.init = function (options) {
            _this.options = options;
            _this.$(window).click(function () { _this.options.boxSuggestion.addClass('hidden'); });
            var iframe = _this.$('#ifr-main');
            iframe.on('load', function () {
                var f = iframe[0];
                var iframeWindow = f.contentWindow || f.contentDocument;
                _this.$(iframeWindow.document).on('click touchend', function () { _this.options.boxSuggestion.addClass('hidden'); });
            });
        };
        this.suggest = function () {
            var keyword = _this.options.elmSearch.val();
            if (keyword.length <= _this.minSearchLength) {
                _this.options.boxSuggestion.addClass('hidden');
                return;
            }
            _this.search(keyword);
        };
        this.search = function (keyword) {
            var self = _this;
            _this.$.ajax({
                type: 'GET',
                url: _this.options.suggestUrl + "?keyword=" + keyword,
                success: function (data, textStatus, jqXHR) {
                    var ul = _this.options.boxSuggestion.find(">ul");
                    ul.empty();
                    if (data.length) {
                        data.forEach(function (e) {
                            ul.append('<li data-id="' + e.Id + '">' + e.Value + '</li>');
                        });
                        ul.find(">li").click(function () {
                            var $this = self.$(this);
                            self.selectedSuggestion($this);
                        });
                    }
                    else {
                        ul.append('<li>' + _this.$("#NoRecords").val() + '</li>');
                    }
                    _this.options.boxSuggestion.removeClass('hidden');
                },
                error: function () {
                    var ul = _this.options.boxSuggestion.find(">ul");
                    ul.empty();
                    ul.append('<li>' + _this.$("#NoRecords").val() + '</li>');
                    _this.options.boxSuggestion.removeClass('hidden');
                },
                complete: function (jqXHR, textStatus) {
                }
            });
        };
        this.selectedSuggestion = function (elm) {
            var id = elm.attr("data-id");
            _this.options.selectedSuggestion(id);
        };
        this.$ = $;
    }
    return AutoSuggestion;
}());
;
//# sourceMappingURL=auto-suggestion.js.map

var oldMainUltilities = {
    isIE: function () {
        return (/(MSIE|Trident\/|Edge\/)/i.test(navigator.userAgent) && !/opera/i.test(navigator.userAgent));
    },
    isSmallDevice: function () {
        var isSmallDevice = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
        return isSmallDevice;
    },
    isiOS: function () {
        var isiOS = /iPhone|iPad|iPod/i.test(navigator.userAgent);
        return isiOS;
    },
    getOffset: function (el) {
        var _x = 0;
        var _y = 0;
        while (el && !isNaN(el.offsetLeft) && !isNaN(el.offsetTop)) {
            if (el.style.display == 'none') {
                _x += parseFloat(el.style.left.replace(/px/g, '')) - el.scrollLeft;
                _y += parseFloat(el.style.top.replace(/px/g, '')) - el.scrollTop;
            } else {
                _x += el.offsetLeft - el.scrollLeft;
                _y += el.offsetTop - el.scrollTop;
            }

            el = el.offsetParent;
        }

        return { top: _y, left: _x };
    },
    getScrollLeft: function (element) {
        return element.pageXOffset || element.scrollX || element.scrollLeft || 0;
    },
    getScrollTop: function (element) {
        return element.pageYOffset || element.scrollY || element.scrollTop || 0;
    }
};

var AgeWnd = function () {
    return {
        popUp: [],
        callback: [],
        id: null,
        iframeAttributes: null,
        calculateWidthHeight: function (width, height, position) {
            var realHeight = height;
            var maxWidth = window.top.window.innerWidth;

            var minHeight = 250;
            var maxHeight = window.top.window.innerHeight - (position ? position.top : 0) - 60;
            maxHeight = maxHeight < minHeight ? minHeight : maxHeight;

            width = width > maxWidth ? maxWidth : width;
            realHeight = realHeight > maxHeight ? maxHeight : realHeight;
            realHeight = realHeight < minHeight ? minHeight : realHeight;

            return {
                width: width,
                height: realHeight
            };
        },
        open: function (url, title, left, popUpTop, width, height, windowCss, titleCss, closeBtnCss, iframeAttributes, options) {
            options = options || {};
            popUpTop = popUpTop || 200;

            var widthHeight = this.calculateWidthHeight(width, height, { top: popUpTop });
            width = widthHeight.width;
            height = widthHeight.height;

            title = title || '';
            var popH = height || 360;
            popH = popH + 50;

            var oldSiteBodyWidthPadding = 34;
            var popW = (width + oldSiteBodyWidthPadding) || 480;
            left = left || 200;

            var popUp = this.popUp.length >= 1 ? this.popUp.pop() : $("#page-popup").clone();

            var popupId = "page-popup-" + Math.random().toString(36).substring(7);
            popUp.attr("id", popupId);
            if (windowCss) {
                popUp.addClass(windowCss);
            }

            this.id = popupId;
            // Calculate body scrolltop to fix issue: focus popup make body scroll to popup position (on device)
            var scrollTop = oldMainUltilities.getScrollTop(window.document.body);
            var scrollLeft = oldMainUltilities.getScrollLeft(window.document.body);

            // ON iOS: there is scroll issues with popup. We must show popup with auto height
            var isAutoHeight = !options.isIgnoreAutoHeightOniOS && oldMainUltilities.isiOS();

            var popupCss = {
                width: popW,
                height: isAutoHeight ? 'auto' : popH,
                left: left + scrollLeft,
                top: popUpTop + scrollTop
            };

            if (options.resizable) {
                popupCss.overflow = "hidden";
            }

            if (isAutoHeight) {
                $.extend(popupCss, { 'overflow-y': 'visible', 'overflow-x': 'auto' });
            }

            popUp.css(popupCss);

            var iframe = popUp.find("#popup-iframe");
            iframe.attr("src", "about:blank");

            if (options.customClass) {
                popUp.addClass(options.customClass);
            }

            popUp.appendTo("body");
            popUp.removeClass("hidden");
            popUp.find(".popup-title").html(title);
            if (!isAutoHeight) {
                popUp.find(".ex-modal").css("height", popH + "px");
            }

            var headerHeight = 40;
            var contentBorder = 10;
            var bodyHeightPadding = 18;
            popUp.find(".modal-content .modal-body").css("height", isAutoHeight ? 'auto' : (popH - headerHeight - contentBorder) + "px");

            var iframeHeight = popH - headerHeight - contentBorder - bodyHeightPadding;
            ageWnd.setIframeInfo(iframe, popUp, iframeHeight, left, popUpTop, isAutoHeight);
            if (url) {
                iframe.attr("src", url);
            } else {
                ageWnd.copyTargetForm(popUp);
                iframe = popUp.find('.modal-body').find('#popup-iframe');
                ageWnd.setIframeInfo(iframe, popUp, iframeHeight, left, popUpTop, isAutoHeight);
            }

            if (iframeAttributes) {
                this.iframeAttributes = iframeAttributes;
                this.iframeAttributes.src = url;
            }

            ageWnd.openPopUpAfterSetIframeSrc(popUp, isAutoHeight, this, options);
        },
        openPopUpAfterSetIframeSrc: function (popUp, isAutoHeight, currentAgeWnd, options) {
            function getElementSize(element) {
                var style = element[0].style;
                return {
                    width: parseInt(style.width.substring(0, style.width.length - 2)),
                    height: parseInt(style.height.substring(0, style.height.length - 2))
                };
            }

            popUp.on('shown.bs.modal', function () {
                // transform property make objectFlash not show (MacauGames Result)
                popUp.find('.modal-dialog').css({
                    transform: 'none'
                });

                // This will make popup isn't cut its height when its height larger than browser height
                if (isAutoHeight) {
                    popUp.css({ display: 'table' });
                }
            });

            popUp.modal({ show: true, backdrop: false });

            if (options.resizable) {
                var prevPosition = {};
                var prevSize = {};

                popUp.resizable({
                    minHeight: 100,
                    minWidth: 100,
                    direction: ['right', 'bottom'],
                    start: function () {
                        prevPosition = popUp.offset();
                        prevSize = getElementSize(popUp);
                    },
                    resize: function () {
                        var size = getElementSize(popUp);

                        popUp.find(".ex-modal").css("height", size.height + "px");

                        var headerHeight = 40;
                        var contentBorder = 10;
                        var bodyHeightPadding = 18;
                        popUp.find(".modal-content .modal-body").css("height", (size.height - headerHeight - contentBorder) + "px");
                    },
                    stop: function () {
                        var size = getElementSize(popUp);

                        if (prevSize.width == size.width && prevSize.height == size.height) {
                            return;
                        }

                        prevSize = size;
                        popUp.offset(prevPosition);
                    }
                });
            }

            popUp.find(".icon-close").unbind("click").click(function () {
                popUp.find('.modal-body #target').remove();
                popUp.remove();

                if (currentAgeWnd.onClosing && typeof (currentAgeWnd.onClosing) == "function") {
                    currentAgeWnd.onClosing();
                }
            });

            popUp.tinyDraggable({
                exclude: "a.icon-information-outline.icon-info, button.icon.icon-close.close",
                resizeElement: options.resizable ? ".resizable-handle" : ""
            });

            currentAgeWnd.popUp.push(popUp);
        },
        close: function () {
            var popUp = this.popUp.pop();
            if (!popUp) {
                return;
            }
            popUp.find('.modal-body #target').remove();
            popUp.remove();

            if (this.onClosing && typeof (this.onClosing) == "function") {
                this.onClosing();
            }
        },
        setRect: function (left, top, width, height) {
        },
        createInstance: function () {
            return new AgeWnd();
        },

        copyTargetForm: function (popUp) {
            popUp.find('.modal-body').find("#target").remove();

            var oldTarget = top.main.document.getElementById("target");
            var newTarget = $.clone(oldTarget);

            popUp.find('.modal-body').find('#popup-iframe').remove();
            var iframeId = 'edit-setting-frame-' + Math.random().toString(36).substring(7);
            popUp.find('.modal-body').append('<iframe id="popup-iframe" frameborder="0" allowtransparency="yes" marginheight="0" marginwidth="0" scrolling="auto" name="' + iframeId + '"></iframe>');

            popUp.find('.modal-body').append(newTarget);
            newTarget.target = iframeId;
            newTarget.submit();
        },
        setIframeInfo: function (iframe, popUp, iframeHeight, left, top, isAutoHeight) {
            iframe.css({
                width: '100%',
                height: '100%',
                left: left,
                top: top
            });

            iframe.load(function () {
                ageWnd.updatePopupTitleAfterLoad(popUp, iframe);

                if (oldMainUltilities.isSmallDevice()) {
                    // This will make elements on popup lose focus
                    // Issue: click any point on popup trigger open keyboard/dropdown (default focus elements)
                    iframe.contents().find(':focus').blur();
                }

                ageWnd.fixIOSHeight(iframe);
            });
        },
        updatePopupTitleAfterLoad: function (popUp, iframe) {
            try {
                var title = popUp.find(".popup-title").html() || '';
                var iframe_doc = iframe.contents();

                var iframeTitle = iframe_doc.find("title").text();
                if (title == '' && iframeTitle != 'Home') {
                    popUp.find(".popup-title").html(iframeTitle);
                }
            } catch (e) { }
        },
        fixIOSHeight: function (iframe) {
            var isAutoHeight = oldMainUltilities.isiOS();
            if (!isAutoHeight) {
                return;
            }

            var fixHeightInterval = setInterval(function () {
                var iframe0 = iframe[0];
                var iframeDoc = iframe0.contentDocument || (iframe0.contentWindow ? iframe0.contentWindow.document : null);
                if (!iframeDoc) {
                    return;
                }

                var isReady = iframeDoc.readyState == 'complete' || iframeDoc.readyState == 'interactive';
                if (!isReady) {
                    return;
                }

                var iframeBody = iframe.contents().find('body');
                var bodyCss = {
                    height: 'auto',
                    position: "relative",
                    float: 'left'
                };

                iframeBody.css(bodyCss);

                if (iframeBody.height()) {
                    iframeBody.css(bodyCss);
                    var maxHeight = $(window).height() - 200;
                    var minHeight = iframeBody.height() + 100;

                    iframe.css({
                        "min-height": Math.min(minHeight, maxHeight) + "px"
                    });

                    iframeBody.css({
                        float: 'none'
                    });

                    clearInterval(fixHeightInterval);
                }
            }, 200);
        }
    }
};

window.ageWnd = window.ageWnd || new AgeWnd();

var popupManager = {
    positionPadding: 5,
    leftPosition: function () {
        // top-left main frame
        return 340 + top.popupManager.positionPadding;
    },

    topPosition: function () {
        // top-left main frame
        return 67 + top.popupManager.positionPadding;
    },

    // Choose most suitable screen resolution 1920 X 1080
    defaultHeight: 1000,

    defaultWidth: 480,
    openByRelativeUrl: function (url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes, options) {
        window.top.popupManager.open(age.GetBaseUrl() + url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes, options);
    },
    openMultipleByRelativeUrl: function (url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes, options) {
        window.top.popupManager.openMultiple(age.GetBaseUrl() + url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes, options);
    },
    open: function (url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes, options) {
        var handler = window.top.ageWnd;
        window.top.popupManager.openWithHanlder(handler, url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes, options);
    },
    openMultiple: function (url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes, options) {
        var handler = window.top.ageWnd.createInstance();
        window.top.popupManager.openWithHanlder(handler, url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes, options);
    },
    openWithHanlder: function (handler, url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes, options) {
        left = left || window.top.popupManager.leftPosition();
        top = top || window.top.popupManager.topPosition();

        width = width || window.top.popupManager.defaultWidth;
        height = height || window.top.popupManager.defaultHeight;
        title = title || '';

        handler.open(url, title, left, top, width, height, windowCss, titleCss, closeBtnCss, iframeAttributes, options);
    },
    close: function () {
        top.ageWnd.close();
    },
    closeAllPopup: function () {
        top.$('.modal[id^="page-popup-"]').remove();
    }
};

/// <reference path="auto-suggestion.ts" />
var DefaultPage = /** @class */ (function () {
    function DefaultPage($, topWindow, autoSuggestion, popupManager) {
        var _this = this;
        this.enterKeyCode = 13;
        this.currentUrlCookieName = 'DFFKDHFJKLDHFJDH2';
        this.recheckInIntervalMiliseconds = 30000;
        this.init = function () {
            _this.registerEvents();
            _this.autoSuggestion.init({
                elmSearch: _this.$('#txt-search'),
                boxSuggestion: _this.$('.box-suggestion'),
                suggestUrl: _this.$("#RootPath").val() + "Article/Suggest",
                selectedSuggestion: _this.selectedSuggestion
            });
            var setCurrentURL = _this.$("#SetCurrentURL").val() === '1';
            if (setCurrentURL) {
                var currentUrl = _this.getCurrentUrl();
                var mainSrc = currentUrl && currentUrl.indexOf("_Authorization/SignIn.aspx") === -1
                    ? currentUrl : "Home";
                _this.$("#ifr-main").attr("src", mainSrc);
            }
            _this.initFeedbackFunction(undefined, 470);
            window.onfocus = function () { _this.reCheckin(); };
            _this.$("#ifr-main").on('load.newmain', function () { _this.reCheckin(); });
            try {
                document.addEventListener("visibilitychange", function () { !document.hidden && _this.reCheckin(); });
            }
            catch (e) { }
        };
        this.registerEvents = function () {
            _this.$('.btn-search').click(function () { return _this.searchArticle(); });
            _this.$('#txt-search').keyup(function (ev) {
                var keycode = (ev.keyCode ? ev.keyCode : ev.which);
                if (keycode === _this.enterKeyCode) {
                    _this.searchArticle();
                    return;
                }
                _this.autoSuggestion.suggest();
            });
            _this.$(".language-container").click(_this.showHideLanguage);
            var self = _this;
            _this.$(".dropdown-language > li a").click(function () {
                var s = self.$(this).attr('data-val');
                self.changeLanguage(s);
            });
            _this.$(".item-cat").click(function () {
                var $this = self.$(this);
                if ($this.closest('li').find('>.item-cat').hasClass('open')) {
                    $this.closest('li').find('>.item-cat').removeClass('open');
                    return;
                }
                $this.closest('.list-item-cat').find('>.item-cat').removeClass('open');
                $this.closest('li').find('>.item-cat').addClass('open');
            });
            _this.$(".item-slug").click(function () {
                self.$(".item-cat.active").removeClass('active');
                var $this = self.$(this);
                $this.parents('.item-cat').addClass('active');
            });
            _this.$("#ifr-main").on('load', function () {
                self.cacheCurrentUrl();
            });
            _this.reCheckin();
            setTimeout(function () { _this.reCheckin(); }, _this.recheckInIntervalMiliseconds);
        };
        this.searchArticle = function () {
            var keyword = _this.$('#txt-search').val();
            if (keyword.length <= 2) {
                return;
            }
            _this.$("#ifr-main").attr("src", _this.$("#RootPath").val() + "Article/Search?keyword=" + keyword);
        };
        this.showHideLanguage = function () {
            var ddl = _this.$(".dropdown-language");
            if (ddl.hasClass('open')) {
                ddl.removeClass('open');
            }
            else {
                ddl.addClass('open');
            }
        };
        this.changeLanguage = function (s) {
            _this.$.ajax({
                type: 'GET',
                url: _this.$("#RootPath").val() + "Language/Change?selected=" + s,
                success: function (data, textStatus, jqXHR) {
                    var currentURL = _this.getCurrentUrl();
                    if (currentURL.indexOf('/Article/GetBySlug?') === -1) {
                        _this.topWindow.location.reload(true);
                    }
                    else {
                        _this.topWindow.location.href = currentURL.replace('/Article/GetBySlug?', '/Default/GetBySlug?');
                    }
                },
                complete: function (jqXHR, textStatus) { }
            });
        };
        this.getCurrentUrl = function () {
            return localStorage.getItem(_this.currentUrlCookieName);
        };
        this.cacheCurrentUrl = function () {
            try {
                var mainFrame = document.getElementById("ifr-main");
                var mainFrameDocument = mainFrame.contentWindow || mainFrame.contentDocument;
                var currentUrl = mainFrameDocument && mainFrameDocument.location.href || mainFrame.src;
                localStorage.setItem(_this.currentUrlCookieName, currentUrl);
            }
            catch (e) { }
        };
        this.reCheckin = function () {
            // Post with return result in JSON format
            _this.$.cookie('ssl', location.href.indexOf('https://') === 0, { expires: 30, path: '/' });
            var self = _this;
            _this.$.ajax({
                type: "POST",
                url: _this.$("#OldMainRootPath").val() + '_Authorization/Handlers/ReCheckin.ashx',
                data: { onid: _this.$("#OnUserId").val() },
                success: function (result) {
                    try {
                        result = eval("(" + result + ")");
                        var errCode = result.errCode;
                        var errMsg = result.errMsg;
                        if (errCode !== 0) {
                            //self.topWindow.location.reload(true);
                            self.topWindow.location = self.$("#OldMainRootPath").val() + "/_Authorization/SignIn.aspx";
                        }
                    }
                    catch (e) {
                    }
                },
                complete: function () {
                    setTimeout(function () { _this.reCheckin(); }, _this.recheckInIntervalMiliseconds);
                }
            });
        };
        this.selectedSuggestion = function (id) {
            var url = _this.$("#RootPath").val() + "Article/GetBySlugSuggest";
            _this.$.ajax({
                type: "POST",
                url: url,
                data: { slug: id },
                success: function (result) {
                    if (result.TargetFrame == 'main') {
                        var articleURL = _this.$("#RootPath").val() + "Article/GetBySlug?slug=" + id;
                        _this.$("#ifr-main").attr("src", articleURL);
                    }
                    else {
                        window.open(result.Url);
                    }
                },
                complete: function () {
                }
            });
        };
        this.getCustomerCookiesName = function (key) {
            var custId = _this.$("#CustId").val();
            var subAccId = _this.$("#SubAccId").val();
            return key + "_" + custId + "_" + subAccId;
        };
        this.initFeedbackFunction = function (width, height) {
            var hideFeedbackFunctionSessionKey = _this.getCustomerCookiesName('HideFeedbackFunction_Handbook');
            if (sessionStorage.getItem(hideFeedbackFunctionSessionKey) === "1") {
                return;
            }
            var functionId = 8;
            var url = _this.$("#MessageRootPath").val() + 'Feedback/CheckExist?id=' + functionId;
            _this.$.get(url, function (result) {
                if (result.IsExist) {
                    return;
                }
                var feedbackFunction = _this.$("#feedback-function");
                var feedbackBox = feedbackFunction.find(".feedback-box");
                feedbackBox.find(".txt-feedback").text(result.FunctionName || "Feedback");
                feedbackBox
                    .off()
                    .click(function () {
                    var url = _this.$("#MessageRootPath").val() + 'Feedback/Function?id=' + functionId;
                    _this.popupManager.open(url, undefined, width || 440, height || 380, null, null, null, null, null, null, { customClass: "feedback-modal" });
                });
                feedbackFunction.find(".icon-close-feedback")
                    .off()
                    .click(function () {
                    sessionStorage.setItem(hideFeedbackFunctionSessionKey, "1");
                    feedbackFunction.addClass("hidden");
                });
                feedbackFunction.removeClass("hidden");
            });
        };
        this.$ = $;
        this.topWindow = topWindow;
        this.autoSuggestion = autoSuggestion;
        this.popupManager = popupManager;
    }
    return DefaultPage;
}());
;
$(function () {
    var autoSuggestion = new AutoSuggestion($);
    var page = new DefaultPage($, window.top, autoSuggestion, popupManager);
    page.init();
});
//# sourceMappingURL=default.js.map