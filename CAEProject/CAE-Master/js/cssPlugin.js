"use strict";

function _typeof(obj) { "@babel/helpers - typeof"; if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

/*!
 * VERSION: 1.13.2
 * DATE: 2014-08-23
 * UPDATES AND DOCS AT: http://www.greensock.com
 *
 * @license Copyright (c) 2008-2014, GreenSock. All rights reserved.
 * This work is subject to the terms at http://www.greensock.com/terms_of_use.html or for
 * Club GreenSock members, the software agreement that was issued with your membership.
 * 
 * @author: Jack Doyle, jack@greensock.com
 */
var _gsScope = "undefined" != typeof module && module.exports && "undefined" != typeof global ? global : void 0 || window;

(_gsScope._gsQueue || (_gsScope._gsQueue = [])).push(function () {
  "use strict";

  _gsScope._gsDefine("plugins.CSSPlugin", ["plugins.TweenPlugin", "TweenLite"], function (t, e) {
    var i,
        r,
        s,
        n,
        a = function a() {
      t.call(this, "css"), this._overwriteProps.length = 0, this.setRatio = a.prototype.setRatio;
    },
        o = {},
        l = a.prototype = new t("css");

    l.constructor = a, a.version = "1.13.2", a.API = 2, a.defaultTransformPerspective = 0, a.defaultSkewType = "compensated", l = "px", a.suffixMap = {
      top: l,
      right: l,
      bottom: l,
      left: l,
      width: l,
      height: l,
      fontSize: l,
      padding: l,
      margin: l,
      perspective: l,
      lineHeight: ""
    };

    var h,
        u,
        f,
        p,
        _,
        c,
        d = /(?:\d|\-\d|\.\d|\-\.\d)+/g,
        m = /(?:\d|\-\d|\.\d|\-\.\d|\+=\d|\-=\d|\+=.\d|\-=\.\d)+/g,
        g = /(?:\+=|\-=|\-|\b)[\d\-\.]+[a-zA-Z0-9]*(?:%|\b)/gi,
        v = /[^\d\-\.]/g,
        y = /(?:\d|\-|\+|=|#|\.)*/g,
        T = /opacity *= *([^)]*)/i,
        x = /opacity:([^;]*)/i,
        w = /alpha\(opacity *=.+?\)/i,
        b = /^(rgb|hsl)/,
        P = /([A-Z])/g,
        S = /-([a-z])/gi,
        k = /(^(?:url\(\"|url\())|(?:(\"\))$|\)$)/gi,
        R = function R(t, e) {
      return e.toUpperCase();
    },
        C = /(?:Left|Right|Width)/i,
        A = /(M11|M12|M21|M22)=[\d\-\.e]+/gi,
        O = /progid\:DXImageTransform\.Microsoft\.Matrix\(.+?\)/i,
        D = /,(?=[^\)]*(?:\(|$))/gi,
        M = Math.PI / 180,
        L = 180 / Math.PI,
        N = {},
        z = document,
        X = z.createElement("div"),
        I = z.createElement("img"),
        E = a._internals = {
      _specialProps: o
    },
        F = navigator.userAgent,
        Y = function () {
      var t,
          e = F.indexOf("Android"),
          i = z.createElement("div");
      return f = -1 !== F.indexOf("Safari") && -1 === F.indexOf("Chrome") && (-1 === e || Number(F.substr(e + 8, 1)) > 3), _ = f && 6 > Number(F.substr(F.indexOf("Version/") + 8, 1)), p = -1 !== F.indexOf("Firefox"), /MSIE ([0-9]{1,}[\.0-9]{0,})/.exec(F) && (c = parseFloat(RegExp.$1)), i.innerHTML = "<a style='top:1px;opacity:.55;'>a</a>", t = i.getElementsByTagName("a")[0], t ? /^0.55/.test(t.style.opacity) : !1;
    }(),
        B = function B(t) {
      return T.test("string" == typeof t ? t : (t.currentStyle ? t.currentStyle.filter : t.style.filter) || "") ? parseFloat(RegExp.$1) / 100 : 1;
    },
        U = function U(t) {
      window.console && console.log(t);
    },
        j = "",
        W = "",
        V = function V(t, e) {
      e = e || X;
      var i,
          r,
          s = e.style;
      if (void 0 !== s[t]) return t;

      for (t = t.charAt(0).toUpperCase() + t.substr(1), i = ["O", "Moz", "ms", "Ms", "Webkit"], r = 5; --r > -1 && void 0 === s[i[r] + t];) {
        ;
      }

      return r >= 0 ? (W = 3 === r ? "ms" : i[r], j = "-" + W.toLowerCase() + "-", W + t) : null;
    },
        q = z.defaultView ? z.defaultView.getComputedStyle : function () {},
        H = a.getStyle = function (t, e, i, r, s) {
      var n;
      return Y || "opacity" !== e ? (!r && t.style[e] ? n = t.style[e] : (i = i || q(t)) ? n = i[e] || i.getPropertyValue(e) || i.getPropertyValue(e.replace(P, "-$1").toLowerCase()) : t.currentStyle && (n = t.currentStyle[e]), null == s || n && "none" !== n && "auto" !== n && "auto auto" !== n ? n : s) : B(t);
    },
        Q = E.convertToPixels = function (t, i, r, s, n) {
      if ("px" === s || !s) return r;
      if ("auto" === s || !r) return 0;

      var o,
          l,
          h,
          u = C.test(i),
          f = t,
          p = X.style,
          _ = 0 > r;

      if (_ && (r = -r), "%" === s && -1 !== i.indexOf("border")) o = r / 100 * (u ? t.clientWidth : t.clientHeight);else {
        if (p.cssText = "border:0 solid red;position:" + H(t, "position") + ";line-height:0;", "%" !== s && f.appendChild) p[u ? "borderLeftWidth" : "borderTopWidth"] = r + s;else {
          if (f = t.parentNode || z.body, l = f._gsCache, h = e.ticker.frame, l && u && l.time === h) return l.width * r / 100;
          p[u ? "width" : "height"] = r + s;
        }
        f.appendChild(X), o = parseFloat(X[u ? "offsetWidth" : "offsetHeight"]), f.removeChild(X), u && "%" === s && a.cacheWidths !== !1 && (l = f._gsCache = f._gsCache || {}, l.time = h, l.width = 100 * (o / r)), 0 !== o || n || (o = Q(t, i, r, s, !0));
      }
      return _ ? -o : o;
    },
        G = E.calculateOffset = function (t, e, i) {
      if ("absolute" !== H(t, "position", i)) return 0;
      var r = "left" === e ? "Left" : "Top",
          s = H(t, "margin" + r, i);
      return t["offset" + r] - (Q(t, e, parseFloat(s), s.replace(y, "")) || 0);
    },
        Z = function Z(t, e) {
      var i,
          r,
          s = {};
      if (e = e || q(t, null)) {
        if (i = e.length) for (; --i > -1;) {
          s[e[i].replace(S, R)] = e.getPropertyValue(e[i]);
        } else for (i in e) {
          s[i] = e[i];
        }
      } else if (e = t.currentStyle || t.style) for (i in e) {
        "string" == typeof i && void 0 === s[i] && (s[i.replace(S, R)] = e[i]);
      }
      return Y || (s.opacity = B(t)), r = Pe(t, e, !1), s.rotation = r.rotation, s.skewX = r.skewX, s.scaleX = r.scaleX, s.scaleY = r.scaleY, s.x = r.x, s.y = r.y, we && (s.z = r.z, s.rotationX = r.rotationX, s.rotationY = r.rotationY, s.scaleZ = r.scaleZ), s.filters && delete s.filters, s;
    },
        $ = function $(t, e, i, r, s) {
      var n,
          a,
          o,
          l = {},
          h = t.style;

      for (a in i) {
        "cssText" !== a && "length" !== a && isNaN(a) && (e[a] !== (n = i[a]) || s && s[a]) && -1 === a.indexOf("Origin") && ("number" == typeof n || "string" == typeof n) && (l[a] = "auto" !== n || "left" !== a && "top" !== a ? "" !== n && "auto" !== n && "none" !== n || "string" != typeof e[a] || "" === e[a].replace(v, "") ? n : 0 : G(t, a), void 0 !== h[a] && (o = new fe(h, a, h[a], o)));
      }

      if (r) for (a in r) {
        "className" !== a && (l[a] = r[a]);
      }
      return {
        difs: l,
        firstMPT: o
      };
    },
        K = {
      width: ["Left", "Right"],
      height: ["Top", "Bottom"]
    },
        J = ["marginLeft", "marginRight", "marginTop", "marginBottom"],
        te = function te(t, e, i) {
      var r = parseFloat("width" === e ? t.offsetWidth : t.offsetHeight),
          s = K[e],
          n = s.length;

      for (i = i || q(t, null); --n > -1;) {
        r -= parseFloat(H(t, "padding" + s[n], i, !0)) || 0, r -= parseFloat(H(t, "border" + s[n] + "Width", i, !0)) || 0;
      }

      return r;
    },
        ee = function ee(t, e) {
      (null == t || "" === t || "auto" === t || "auto auto" === t) && (t = "0 0");
      var i = t.split(" "),
          r = -1 !== t.indexOf("left") ? "0%" : -1 !== t.indexOf("right") ? "100%" : i[0],
          s = -1 !== t.indexOf("top") ? "0%" : -1 !== t.indexOf("bottom") ? "100%" : i[1];
      return null == s ? s = "0" : "center" === s && (s = "50%"), ("center" === r || isNaN(parseFloat(r)) && -1 === (r + "").indexOf("=")) && (r = "50%"), e && (e.oxp = -1 !== r.indexOf("%"), e.oyp = -1 !== s.indexOf("%"), e.oxr = "=" === r.charAt(1), e.oyr = "=" === s.charAt(1), e.ox = parseFloat(r.replace(v, "")), e.oy = parseFloat(s.replace(v, ""))), r + " " + s + (i.length > 2 ? " " + i[2] : "");
    },
        ie = function ie(t, e) {
      return "string" == typeof t && "=" === t.charAt(1) ? parseInt(t.charAt(0) + "1", 10) * parseFloat(t.substr(2)) : parseFloat(t) - parseFloat(e);
    },
        re = function re(t, e) {
      return null == t ? e : "string" == typeof t && "=" === t.charAt(1) ? parseInt(t.charAt(0) + "1", 10) * Number(t.substr(2)) + e : parseFloat(t);
    },
        se = function se(t, e, i, r) {
      var s,
          n,
          a,
          o,
          l = 1e-6;
      return null == t ? o = e : "number" == typeof t ? o = t : (s = 360, n = t.split("_"), a = Number(n[0].replace(v, "")) * (-1 === t.indexOf("rad") ? 1 : L) - ("=" === t.charAt(1) ? 0 : e), n.length && (r && (r[i] = e + a), -1 !== t.indexOf("short") && (a %= s, a !== a % (s / 2) && (a = 0 > a ? a + s : a - s)), -1 !== t.indexOf("_cw") && 0 > a ? a = (a + 9999999999 * s) % s - (0 | a / s) * s : -1 !== t.indexOf("ccw") && a > 0 && (a = (a - 9999999999 * s) % s - (0 | a / s) * s)), o = e + a), l > o && o > -l && (o = 0), o;
    },
        ne = {
      aqua: [0, 255, 255],
      lime: [0, 255, 0],
      silver: [192, 192, 192],
      black: [0, 0, 0],
      maroon: [128, 0, 0],
      teal: [0, 128, 128],
      blue: [0, 0, 255],
      navy: [0, 0, 128],
      white: [255, 255, 255],
      fuchsia: [255, 0, 255],
      olive: [128, 128, 0],
      yellow: [255, 255, 0],
      orange: [255, 165, 0],
      gray: [128, 128, 128],
      purple: [128, 0, 128],
      green: [0, 128, 0],
      red: [255, 0, 0],
      pink: [255, 192, 203],
      cyan: [0, 255, 255],
      transparent: [255, 255, 255, 0]
    },
        ae = function ae(t, e, i) {
      return t = 0 > t ? t + 1 : t > 1 ? t - 1 : t, 0 | 255 * (1 > 6 * t ? e + 6 * (i - e) * t : .5 > t ? i : 2 > 3 * t ? e + 6 * (i - e) * (2 / 3 - t) : e) + .5;
    },
        oe = function oe(t) {
      var e, i, r, s, n, a;
      return t && "" !== t ? "number" == typeof t ? [t >> 16, 255 & t >> 8, 255 & t] : ("," === t.charAt(t.length - 1) && (t = t.substr(0, t.length - 1)), ne[t] ? ne[t] : "#" === t.charAt(0) ? (4 === t.length && (e = t.charAt(1), i = t.charAt(2), r = t.charAt(3), t = "#" + e + e + i + i + r + r), t = parseInt(t.substr(1), 16), [t >> 16, 255 & t >> 8, 255 & t]) : "hsl" === t.substr(0, 3) ? (t = t.match(d), s = Number(t[0]) % 360 / 360, n = Number(t[1]) / 100, a = Number(t[2]) / 100, i = .5 >= a ? a * (n + 1) : a + n - a * n, e = 2 * a - i, t.length > 3 && (t[3] = Number(t[3])), t[0] = ae(s + 1 / 3, e, i), t[1] = ae(s, e, i), t[2] = ae(s - 1 / 3, e, i), t) : (t = t.match(d) || ne.transparent, t[0] = Number(t[0]), t[1] = Number(t[1]), t[2] = Number(t[2]), t.length > 3 && (t[3] = Number(t[3])), t)) : ne.black;
    },
        le = "(?:\\b(?:(?:rgb|rgba|hsl|hsla)\\(.+?\\))|\\B#.+?\\b";

    for (l in ne) {
      le += "|" + l + "\\b";
    }

    le = RegExp(le + ")", "gi");

    var he = function he(t, e, i, r) {
      if (null == t) return function (t) {
        return t;
      };
      var s,
          n = e ? (t.match(le) || [""])[0] : "",
          a = t.split(n).join("").match(g) || [],
          o = t.substr(0, t.indexOf(a[0])),
          l = ")" === t.charAt(t.length - 1) ? ")" : "",
          h = -1 !== t.indexOf(" ") ? " " : ",",
          u = a.length,
          f = u > 0 ? a[0].replace(d, "") : "";
      return u ? s = e ? function (t) {
        var e, p, _, c;

        if ("number" == typeof t) t += f;else if (r && D.test(t)) {
          for (c = t.replace(D, "|").split("|"), _ = 0; c.length > _; _++) {
            c[_] = s(c[_]);
          }

          return c.join(",");
        }
        if (e = (t.match(le) || [n])[0], p = t.split(e).join("").match(g) || [], _ = p.length, u > _--) for (; u > ++_;) {
          p[_] = i ? p[0 | (_ - 1) / 2] : a[_];
        }
        return o + p.join(h) + h + e + l + (-1 !== t.indexOf("inset") ? " inset" : "");
      } : function (t) {
        var e, n, p;
        if ("number" == typeof t) t += f;else if (r && D.test(t)) {
          for (n = t.replace(D, "|").split("|"), p = 0; n.length > p; p++) {
            n[p] = s(n[p]);
          }

          return n.join(",");
        }
        if (e = t.match(g) || [], p = e.length, u > p--) for (; u > ++p;) {
          e[p] = i ? e[0 | (p - 1) / 2] : a[p];
        }
        return o + e.join(h) + l;
      } : function (t) {
        return t;
      };
    },
        ue = function ue(t) {
      return t = t.split(","), function (e, i, r, s, n, a, o) {
        var l,
            h = (i + "").split(" ");

        for (o = {}, l = 0; 4 > l; l++) {
          o[t[l]] = h[l] = h[l] || h[(l - 1) / 2 >> 0];
        }

        return s.parse(e, o, n, a);
      };
    },
        fe = (E._setPluginRatio = function (t) {
      this.plugin.setRatio(t);

      for (var e, i, r, s, n = this.data, a = n.proxy, o = n.firstMPT, l = 1e-6; o;) {
        e = a[o.v], o.r ? e = Math.round(e) : l > e && e > -l && (e = 0), o.t[o.p] = e, o = o._next;
      }

      if (n.autoRotate && (n.autoRotate.rotation = a.rotation), 1 === t) for (o = n.firstMPT; o;) {
        if (i = o.t, i.type) {
          if (1 === i.type) {
            for (s = i.xs0 + i.s + i.xs1, r = 1; i.l > r; r++) {
              s += i["xn" + r] + i["xs" + (r + 1)];
            }

            i.e = s;
          }
        } else i.e = i.s + i.xs0;

        o = o._next;
      }
    }, function (t, e, i, r, s) {
      this.t = t, this.p = e, this.v = i, this.r = s, r && (r._prev = this, this._next = r);
    }),
        pe = (E._parseToProxy = function (t, e, i, r, s, n) {
      var a,
          o,
          l,
          h,
          u,
          f = r,
          p = {},
          _ = {},
          c = i._transform,
          d = N;

      for (i._transform = null, N = e, r = u = i.parse(t, e, r, s), N = d, n && (i._transform = c, f && (f._prev = null, f._prev && (f._prev._next = null))); r && r !== f;) {
        if (1 >= r.type && (o = r.p, _[o] = r.s + r.c, p[o] = r.s, n || (h = new fe(r, "s", o, h, r.r), r.c = 0), 1 === r.type)) for (a = r.l; --a > 0;) {
          l = "xn" + a, o = r.p + "_" + l, _[o] = r.data[l], p[o] = r[l], n || (h = new fe(r, l, o, h, r.rxp[l]));
        }
        r = r._next;
      }

      return {
        proxy: p,
        end: _,
        firstMPT: h,
        pt: u
      };
    }, E.CSSPropTween = function (t, e, r, s, a, o, l, h, u, f, p) {
      this.t = t, this.p = e, this.s = r, this.c = s, this.n = l || e, t instanceof pe || n.push(this.n), this.r = h, this.type = o || 0, u && (this.pr = u, i = !0), this.b = void 0 === f ? r : f, this.e = void 0 === p ? r + s : p, a && (this._next = a, a._prev = this);
    }),
        _e = a.parseComplex = function (t, e, i, r, s, n, a, o, l, u) {
      i = i || n || "", a = new pe(t, e, 0, 0, a, u ? 2 : 1, null, !1, o, i, r), r += "";

      var f,
          p,
          _,
          c,
          g,
          v,
          y,
          T,
          x,
          w,
          P,
          S,
          k = i.split(", ").join(",").split(" "),
          R = r.split(", ").join(",").split(" "),
          C = k.length,
          A = h !== !1;

      for ((-1 !== r.indexOf(",") || -1 !== i.indexOf(",")) && (k = k.join(" ").replace(D, ", ").split(" "), R = R.join(" ").replace(D, ", ").split(" "), C = k.length), C !== R.length && (k = (n || "").split(" "), C = k.length), a.plugin = l, a.setRatio = u, f = 0; C > f; f++) {
        if (c = k[f], g = R[f], T = parseFloat(c), T || 0 === T) a.appendXtra("", T, ie(g, T), g.replace(m, ""), A && -1 !== g.indexOf("px"), !0);else if (s && ("#" === c.charAt(0) || ne[c] || b.test(c))) S = "," === g.charAt(g.length - 1) ? ")," : ")", c = oe(c), g = oe(g), x = c.length + g.length > 6, x && !Y && 0 === g[3] ? (a["xs" + a.l] += a.l ? " transparent" : "transparent", a.e = a.e.split(R[f]).join("transparent")) : (Y || (x = !1), a.appendXtra(x ? "rgba(" : "rgb(", c[0], g[0] - c[0], ",", !0, !0).appendXtra("", c[1], g[1] - c[1], ",", !0).appendXtra("", c[2], g[2] - c[2], x ? "," : S, !0), x && (c = 4 > c.length ? 1 : c[3], a.appendXtra("", c, (4 > g.length ? 1 : g[3]) - c, S, !1)));else if (v = c.match(d)) {
          if (y = g.match(m), !y || y.length !== v.length) return a;

          for (_ = 0, p = 0; v.length > p; p++) {
            P = v[p], w = c.indexOf(P, _), a.appendXtra(c.substr(_, w - _), Number(P), ie(y[p], P), "", A && "px" === c.substr(w + P.length, 2), 0 === p), _ = w + P.length;
          }

          a["xs" + a.l] += c.substr(_);
        } else a["xs" + a.l] += a.l ? " " + c : c;
      }

      if (-1 !== r.indexOf("=") && a.data) {
        for (S = a.xs0 + a.data.s, f = 1; a.l > f; f++) {
          S += a["xs" + f] + a.data["xn" + f];
        }

        a.e = S + a["xs" + f];
      }

      return a.l || (a.type = -1, a.xs0 = a.e), a.xfirst || a;
    },
        ce = 9;

    for (l = pe.prototype, l.l = l.pr = 0; --ce > 0;) {
      l["xn" + ce] = 0, l["xs" + ce] = "";
    }

    l.xs0 = "", l._next = l._prev = l.xfirst = l.data = l.plugin = l.setRatio = l.rxp = null, l.appendXtra = function (t, e, i, r, s, n) {
      var a = this,
          o = a.l;
      return a["xs" + o] += n && o ? " " + t : t || "", i || 0 === o || a.plugin ? (a.l++, a.type = a.setRatio ? 2 : 1, a["xs" + a.l] = r || "", o > 0 ? (a.data["xn" + o] = e + i, a.rxp["xn" + o] = s, a["xn" + o] = e, a.plugin || (a.xfirst = new pe(a, "xn" + o, e, i, a.xfirst || a, 0, a.n, s, a.pr), a.xfirst.xs0 = 0), a) : (a.data = {
        s: e + i
      }, a.rxp = {}, a.s = e, a.c = i, a.r = s, a)) : (a["xs" + o] += e + (r || ""), a);
    };

    var de = function de(t, e) {
      e = e || {}, this.p = e.prefix ? V(t) || t : t, o[t] = o[this.p] = this, this.format = e.formatter || he(e.defaultValue, e.color, e.collapsible, e.multi), e.parser && (this.parse = e.parser), this.clrs = e.color, this.multi = e.multi, this.keyword = e.keyword, this.dflt = e.defaultValue, this.pr = e.priority || 0;
    },
        me = E._registerComplexSpecialProp = function (t, e, i) {
      "object" != _typeof(e) && (e = {
        parser: i
      });
      var r,
          s,
          n = t.split(","),
          a = e.defaultValue;

      for (i = i || [a], r = 0; n.length > r; r++) {
        e.prefix = 0 === r && e.prefix, e.defaultValue = i[r] || a, s = new de(n[r], e);
      }
    },
        ge = function ge(t) {
      if (!o[t]) {
        var e = t.charAt(0).toUpperCase() + t.substr(1) + "Plugin";
        me(t, {
          parser: function parser(t, i, r, s, n, a, l) {
            var h = (_gsScope.GreenSockGlobals || _gsScope).com.greensock.plugins[e];
            return h ? (h._cssRegister(), o[r].parse(t, i, r, s, n, a, l)) : (U("Error: " + e + " js file not loaded."), n);
          }
        });
      }
    };

    l = de.prototype, l.parseComplex = function (t, e, i, r, s, n) {
      var a,
          o,
          l,
          h,
          u,
          f,
          p = this.keyword;

      if (this.multi && (D.test(i) || D.test(e) ? (o = e.replace(D, "|").split("|"), l = i.replace(D, "|").split("|")) : p && (o = [e], l = [i])), l) {
        for (h = l.length > o.length ? l.length : o.length, a = 0; h > a; a++) {
          e = o[a] = o[a] || this.dflt, i = l[a] = l[a] || this.dflt, p && (u = e.indexOf(p), f = i.indexOf(p), u !== f && (i = -1 === f ? l : o, i[a] += " " + p));
        }

        e = o.join(", "), i = l.join(", ");
      }

      return _e(t, this.p, e, i, this.clrs, this.dflt, r, this.pr, s, n);
    }, l.parse = function (t, e, i, r, n, a) {
      return this.parseComplex(t.style, this.format(H(t, this.p, s, !1, this.dflt)), this.format(e), n, a);
    }, a.registerSpecialProp = function (t, e, i) {
      me(t, {
        parser: function parser(t, r, s, n, a, o) {
          var l = new pe(t, s, 0, 0, a, 2, s, !1, i);
          return l.plugin = o, l.setRatio = e(t, r, n._tween, s), l;
        },
        priority: i
      });
    };

    var ve = "scaleX,scaleY,scaleZ,x,y,z,skewX,skewY,rotation,rotationX,rotationY,perspective,xPercent,yPercent".split(","),
        ye = V("transform"),
        Te = j + "transform",
        xe = V("transformOrigin"),
        we = null !== V("perspective"),
        be = E.Transform = function () {
      this.skewY = 0;
    },
        Pe = E.getTransform = function (t, e, i, r) {
      if (t._gsTransform && i && !r) return t._gsTransform;

      var s,
          n,
          o,
          l,
          h,
          u,
          f,
          p,
          _,
          c,
          d,
          m,
          g,
          v = i ? t._gsTransform || new be() : new be(),
          y = 0 > v.scaleX,
          T = 2e-5,
          x = 1e5,
          w = 179.99,
          b = w * M,
          P = we ? parseFloat(H(t, xe, e, !1, "0 0 0").split(" ")[2]) || v.zOrigin || 0 : 0,
          S = parseFloat(a.defaultTransformPerspective) || 0;

      if (ye ? s = H(t, Te, e, !0) : t.currentStyle && (s = t.currentStyle.filter.match(A), s = s && 4 === s.length ? [s[0].substr(4), Number(s[2].substr(4)), Number(s[1].substr(4)), s[3].substr(4), v.x || 0, v.y || 0].join(",") : ""), s && "none" !== s && "matrix(1, 0, 0, 1, 0, 0)" !== s) {
        for (n = (s || "").match(/(?:\-|\b)[\d\-\.e]+\b/gi) || [], o = n.length; --o > -1;) {
          l = Number(n[o]), n[o] = (h = l - (l |= 0)) ? (0 | h * x + (0 > h ? -.5 : .5)) / x + l : l;
        }

        if (16 === n.length) {
          var k = n[8],
              R = n[9],
              C = n[10],
              O = n[12],
              D = n[13],
              N = n[14];

          if (v.zOrigin && (N = -v.zOrigin, O = k * N - n[12], D = R * N - n[13], N = C * N + v.zOrigin - n[14]), !i || r || null == v.rotationX) {
            var z,
                X,
                I,
                E,
                F,
                Y,
                B,
                U = n[0],
                j = n[1],
                W = n[2],
                V = n[3],
                q = n[4],
                Q = n[5],
                G = n[6],
                Z = n[7],
                $ = n[11],
                K = Math.atan2(G, C),
                J = -b > K || K > b;
            v.rotationX = K * L, K && (E = Math.cos(-K), F = Math.sin(-K), z = q * E + k * F, X = Q * E + R * F, I = G * E + C * F, k = q * -F + k * E, R = Q * -F + R * E, C = G * -F + C * E, $ = Z * -F + $ * E, q = z, Q = X, G = I), K = Math.atan2(k, U), v.rotationY = K * L, K && (Y = -b > K || K > b, E = Math.cos(-K), F = Math.sin(-K), z = U * E - k * F, X = j * E - R * F, I = W * E - C * F, R = j * F + R * E, C = W * F + C * E, $ = V * F + $ * E, U = z, j = X, W = I), K = Math.atan2(j, Q), v.rotation = K * L, K && (B = -b > K || K > b, E = Math.cos(-K), F = Math.sin(-K), U = U * E + q * F, X = j * E + Q * F, Q = j * -F + Q * E, G = W * -F + G * E, j = X), B && J ? v.rotation = v.rotationX = 0 : B && Y ? v.rotation = v.rotationY = 0 : Y && J && (v.rotationY = v.rotationX = 0), v.scaleX = (0 | Math.sqrt(U * U + j * j) * x + .5) / x, v.scaleY = (0 | Math.sqrt(Q * Q + R * R) * x + .5) / x, v.scaleZ = (0 | Math.sqrt(G * G + C * C) * x + .5) / x, v.skewX = 0, v.perspective = $ ? 1 / (0 > $ ? -$ : $) : 0, v.x = O, v.y = D, v.z = N;
          }
        } else if (!(we && !r && n.length && v.x === n[4] && v.y === n[5] && (v.rotationX || v.rotationY) || void 0 !== v.x && "none" === H(t, "display", e))) {
          var te = n.length >= 6,
              ee = te ? n[0] : 1,
              ie = n[1] || 0,
              re = n[2] || 0,
              se = te ? n[3] : 1;
          v.x = n[4] || 0, v.y = n[5] || 0, u = Math.sqrt(ee * ee + ie * ie), f = Math.sqrt(se * se + re * re), p = ee || ie ? Math.atan2(ie, ee) * L : v.rotation || 0, _ = re || se ? Math.atan2(re, se) * L + p : v.skewX || 0, c = u - Math.abs(v.scaleX || 0), d = f - Math.abs(v.scaleY || 0), Math.abs(_) > 90 && 270 > Math.abs(_) && (y ? (u *= -1, _ += 0 >= p ? 180 : -180, p += 0 >= p ? 180 : -180) : (f *= -1, _ += 0 >= _ ? 180 : -180)), m = (p - v.rotation) % 180, g = (_ - v.skewX) % 180, (void 0 === v.skewX || c > T || -T > c || d > T || -T > d || m > -w && w > m && false | m * x || g > -w && w > g && false | g * x) && (v.scaleX = u, v.scaleY = f, v.rotation = p, v.skewX = _), we && (v.rotationX = v.rotationY = v.z = 0, v.perspective = S, v.scaleZ = 1);
        }

        v.zOrigin = P;

        for (o in v) {
          T > v[o] && v[o] > -T && (v[o] = 0);
        }
      } else v = {
        x: 0,
        y: 0,
        z: 0,
        scaleX: 1,
        scaleY: 1,
        scaleZ: 1,
        skewX: 0,
        perspective: S,
        rotation: 0,
        rotationX: 0,
        rotationY: 0,
        zOrigin: 0
      };

      return i && (t._gsTransform = v), v.xPercent = v.yPercent = 0, v;
    },
        Se = function Se(t) {
      var e,
          i,
          r = this.data,
          s = -r.rotation * M,
          n = s + r.skewX * M,
          a = 1e5,
          o = (0 | Math.cos(s) * r.scaleX * a) / a,
          l = (0 | Math.sin(s) * r.scaleX * a) / a,
          h = (0 | Math.sin(n) * -r.scaleY * a) / a,
          u = (0 | Math.cos(n) * r.scaleY * a) / a,
          f = this.t.style,
          p = this.t.currentStyle;

      if (p) {
        i = l, l = -h, h = -i, e = p.filter, f.filter = "";

        var _,
            d,
            m = this.t.offsetWidth,
            g = this.t.offsetHeight,
            v = "absolute" !== p.position,
            x = "progid:DXImageTransform.Microsoft.Matrix(M11=" + o + ", M12=" + l + ", M21=" + h + ", M22=" + u,
            w = r.x + m * r.xPercent / 100,
            b = r.y + g * r.yPercent / 100;

        if (null != r.ox && (_ = (r.oxp ? .01 * m * r.ox : r.ox) - m / 2, d = (r.oyp ? .01 * g * r.oy : r.oy) - g / 2, w += _ - (_ * o + d * l), b += d - (_ * h + d * u)), v ? (_ = m / 2, d = g / 2, x += ", Dx=" + (_ - (_ * o + d * l) + w) + ", Dy=" + (d - (_ * h + d * u) + b) + ")") : x += ", sizingMethod='auto expand')", f.filter = -1 !== e.indexOf("DXImageTransform.Microsoft.Matrix(") ? e.replace(O, x) : x + " " + e, (0 === t || 1 === t) && 1 === o && 0 === l && 0 === h && 1 === u && (v && -1 === x.indexOf("Dx=0, Dy=0") || T.test(e) && 100 !== parseFloat(RegExp.$1) || -1 === e.indexOf("gradient(" && e.indexOf("Alpha")) && f.removeAttribute("filter")), !v) {
          var P,
              S,
              k,
              R = 8 > c ? 1 : -1;

          for (_ = r.ieOffsetX || 0, d = r.ieOffsetY || 0, r.ieOffsetX = Math.round((m - ((0 > o ? -o : o) * m + (0 > l ? -l : l) * g)) / 2 + w), r.ieOffsetY = Math.round((g - ((0 > u ? -u : u) * g + (0 > h ? -h : h) * m)) / 2 + b), ce = 0; 4 > ce; ce++) {
            S = J[ce], P = p[S], i = -1 !== P.indexOf("px") ? parseFloat(P) : Q(this.t, S, parseFloat(P), P.replace(y, "")) || 0, k = i !== r[S] ? 2 > ce ? -r.ieOffsetX : -r.ieOffsetY : 2 > ce ? _ - r.ieOffsetX : d - r.ieOffsetY, f[S] = (r[S] = Math.round(i - k * (0 === ce || 2 === ce ? 1 : R))) + "px";
          }
        }
      }
    },
        ke = E.set3DTransformRatio = function (t) {
      var e,
          i,
          r,
          s,
          n,
          a,
          o,
          l,
          h,
          u,
          f,
          _,
          c,
          d,
          m,
          g,
          v,
          y,
          T,
          x,
          w,
          b,
          P,
          S = this.data,
          k = this.t.style,
          R = S.rotation * M,
          C = S.scaleX,
          A = S.scaleY,
          O = S.scaleZ,
          D = S.x,
          L = S.y,
          N = S.z,
          z = S.perspective;

      if (!(1 !== t && 0 !== t || "auto" !== S.force3D || S.rotationY || S.rotationX || 1 !== O || z || N)) return Re.call(this, t), void 0;

      if (p) {
        var X = 1e-4;
        X > C && C > -X && (C = O = 2e-5), X > A && A > -X && (A = O = 2e-5), !z || S.z || S.rotationX || S.rotationY || (z = 0);
      }

      if (R || S.skewX) y = Math.cos(R), T = Math.sin(R), e = y, n = T, S.skewX && (R -= S.skewX * M, y = Math.cos(R), T = Math.sin(R), "simple" === S.skewType && (x = Math.tan(S.skewX * M), x = Math.sqrt(1 + x * x), y *= x, T *= x)), i = -T, a = y;else {
        if (!(S.rotationY || S.rotationX || 1 !== O || z)) return k[ye] = (S.xPercent || S.yPercent ? "translate(" + S.xPercent + "%," + S.yPercent + "%) translate3d(" : "translate3d(") + D + "px," + L + "px," + N + "px)" + (1 !== C || 1 !== A ? " scale(" + C + "," + A + ")" : ""), void 0;
        e = a = 1, i = n = 0;
      }
      f = 1, r = s = o = l = h = u = _ = c = d = 0, m = z ? -1 / z : 0, g = S.zOrigin, v = 1e5, R = S.rotationY * M, R && (y = Math.cos(R), T = Math.sin(R), h = f * -T, c = m * -T, r = e * T, o = n * T, f *= y, m *= y, e *= y, n *= y), R = S.rotationX * M, R && (y = Math.cos(R), T = Math.sin(R), x = i * y + r * T, w = a * y + o * T, b = u * y + f * T, P = d * y + m * T, r = i * -T + r * y, o = a * -T + o * y, f = u * -T + f * y, m = d * -T + m * y, i = x, a = w, u = b, d = P), 1 !== O && (r *= O, o *= O, f *= O, m *= O), 1 !== A && (i *= A, a *= A, u *= A, d *= A), 1 !== C && (e *= C, n *= C, h *= C, c *= C), g && (_ -= g, s = r * _, l = o * _, _ = f * _ + g), s = (x = (s += D) - (s |= 0)) ? (0 | x * v + (0 > x ? -.5 : .5)) / v + s : s, l = (x = (l += L) - (l |= 0)) ? (0 | x * v + (0 > x ? -.5 : .5)) / v + l : l, _ = (x = (_ += N) - (_ |= 0)) ? (0 | x * v + (0 > x ? -.5 : .5)) / v + _ : _, k[ye] = (S.xPercent || S.yPercent ? "translate(" + S.xPercent + "%," + S.yPercent + "%) matrix3d(" : "matrix3d(") + [(0 | e * v) / v, (0 | n * v) / v, (0 | h * v) / v, (0 | c * v) / v, (0 | i * v) / v, (0 | a * v) / v, (0 | u * v) / v, (0 | d * v) / v, (0 | r * v) / v, (0 | o * v) / v, (0 | f * v) / v, (0 | m * v) / v, s, l, _, z ? 1 + -_ / z : 1].join(",") + ")";
    },
        Re = E.set2DTransformRatio = function (t) {
      var e,
          i,
          r,
          s,
          n,
          a = this.data,
          o = this.t,
          l = o.style,
          h = a.x,
          u = a.y;
      return a.rotationX || a.rotationY || a.z || a.force3D === !0 || "auto" === a.force3D && 1 !== t && 0 !== t ? (this.setRatio = ke, ke.call(this, t), void 0) : (a.rotation || a.skewX ? (e = a.rotation * M, i = e - a.skewX * M, r = 1e5, s = a.scaleX * r, n = a.scaleY * r, l[ye] = (a.xPercent || a.yPercent ? "translate(" + a.xPercent + "%," + a.yPercent + "%) matrix(" : "matrix(") + (0 | Math.cos(e) * s) / r + "," + (0 | Math.sin(e) * s) / r + "," + (0 | Math.sin(i) * -n) / r + "," + (0 | Math.cos(i) * n) / r + "," + h + "," + u + ")") : l[ye] = (a.xPercent || a.yPercent ? "translate(" + a.xPercent + "%," + a.yPercent + "%) matrix(" : "matrix(") + a.scaleX + ",0,0," + a.scaleY + "," + h + "," + u + ")", void 0);
    };

    me("transform,scale,scaleX,scaleY,scaleZ,x,y,z,rotation,rotationX,rotationY,rotationZ,skewX,skewY,shortRotation,shortRotationX,shortRotationY,shortRotationZ,transformOrigin,transformPerspective,directionalRotation,parseTransform,force3D,skewType,xPercent,yPercent", {
      parser: function parser(t, e, i, r, n, o, l) {
        if (r._transform) return n;

        var h,
            u,
            f,
            p,
            _,
            c,
            d,
            m = r._transform = Pe(t, s, !0, l.parseTransform),
            g = t.style,
            v = 1e-6,
            y = ve.length,
            T = l,
            x = {};

        if ("string" == typeof T.transform && ye) f = X.style, f[ye] = T.transform, f.display = "block", f.position = "absolute", z.body.appendChild(X), h = Pe(X, null, !1), z.body.removeChild(X);else if ("object" == _typeof(T)) {
          if (h = {
            scaleX: re(null != T.scaleX ? T.scaleX : T.scale, m.scaleX),
            scaleY: re(null != T.scaleY ? T.scaleY : T.scale, m.scaleY),
            scaleZ: re(T.scaleZ, m.scaleZ),
            x: re(T.x, m.x),
            y: re(T.y, m.y),
            z: re(T.z, m.z),
            xPercent: re(T.xPercent, m.xPercent),
            yPercent: re(T.yPercent, m.yPercent),
            perspective: re(T.transformPerspective, m.perspective)
          }, d = T.directionalRotation, null != d) if ("object" == _typeof(d)) for (f in d) {
            T[f] = d[f];
          } else T.rotation = d;
          "string" == typeof T.x && -1 !== T.x.indexOf("%") && (h.x = 0, h.xPercent = re(T.x, m.xPercent)), "string" == typeof T.y && -1 !== T.y.indexOf("%") && (h.y = 0, h.yPercent = re(T.y, m.yPercent)), h.rotation = se("rotation" in T ? T.rotation : "shortRotation" in T ? T.shortRotation + "_short" : "rotationZ" in T ? T.rotationZ : m.rotation, m.rotation, "rotation", x), we && (h.rotationX = se("rotationX" in T ? T.rotationX : "shortRotationX" in T ? T.shortRotationX + "_short" : m.rotationX || 0, m.rotationX, "rotationX", x), h.rotationY = se("rotationY" in T ? T.rotationY : "shortRotationY" in T ? T.shortRotationY + "_short" : m.rotationY || 0, m.rotationY, "rotationY", x)), h.skewX = null == T.skewX ? m.skewX : se(T.skewX, m.skewX), h.skewY = null == T.skewY ? m.skewY : se(T.skewY, m.skewY), (u = h.skewY - m.skewY) && (h.skewX += u, h.rotation += u);
        }

        for (we && null != T.force3D && (m.force3D = T.force3D, c = !0), m.skewType = T.skewType || m.skewType || a.defaultSkewType, _ = m.force3D || m.z || m.rotationX || m.rotationY || h.z || h.rotationX || h.rotationY || h.perspective, _ || null == T.scale || (h.scaleZ = 1); --y > -1;) {
          i = ve[y], p = h[i] - m[i], (p > v || -v > p || null != N[i]) && (c = !0, n = new pe(m, i, m[i], p, n), i in x && (n.e = x[i]), n.xs0 = 0, n.plugin = o, r._overwriteProps.push(n.n));
        }

        return p = T.transformOrigin, (p || we && _ && m.zOrigin) && (ye ? (c = !0, i = xe, p = (p || H(t, i, s, !1, "50% 50%")) + "", n = new pe(g, i, 0, 0, n, -1, "transformOrigin"), n.b = g[i], n.plugin = o, we ? (f = m.zOrigin, p = p.split(" "), m.zOrigin = (p.length > 2 && (0 === f || "0px" !== p[2]) ? parseFloat(p[2]) : f) || 0, n.xs0 = n.e = p[0] + " " + (p[1] || "50%") + " 0px", n = new pe(m, "zOrigin", 0, 0, n, -1, n.n), n.b = f, n.xs0 = n.e = m.zOrigin) : n.xs0 = n.e = p) : ee(p + "", m)), c && (r._transformType = _ || 3 === this._transformType ? 3 : 2), n;
      },
      prefix: !0
    }), me("boxShadow", {
      defaultValue: "0px 0px 0px 0px #999",
      prefix: !0,
      color: !0,
      multi: !0,
      keyword: "inset"
    }), me("borderRadius", {
      defaultValue: "0px",
      parser: function parser(t, e, i, n, a) {
        e = this.format(e);

        var o,
            l,
            h,
            u,
            f,
            p,
            _,
            c,
            d,
            m,
            g,
            v,
            y,
            T,
            x,
            w,
            b = ["borderTopLeftRadius", "borderTopRightRadius", "borderBottomRightRadius", "borderBottomLeftRadius"],
            P = t.style;

        for (d = parseFloat(t.offsetWidth), m = parseFloat(t.offsetHeight), o = e.split(" "), l = 0; b.length > l; l++) {
          this.p.indexOf("border") && (b[l] = V(b[l])), f = u = H(t, b[l], s, !1, "0px"), -1 !== f.indexOf(" ") && (u = f.split(" "), f = u[0], u = u[1]), p = h = o[l], _ = parseFloat(f), v = f.substr((_ + "").length), y = "=" === p.charAt(1), y ? (c = parseInt(p.charAt(0) + "1", 10), p = p.substr(2), c *= parseFloat(p), g = p.substr((c + "").length - (0 > c ? 1 : 0)) || "") : (c = parseFloat(p), g = p.substr((c + "").length)), "" === g && (g = r[i] || v), g !== v && (T = Q(t, "borderLeft", _, v), x = Q(t, "borderTop", _, v), "%" === g ? (f = 100 * (T / d) + "%", u = 100 * (x / m) + "%") : "em" === g ? (w = Q(t, "borderLeft", 1, "em"), f = T / w + "em", u = x / w + "em") : (f = T + "px", u = x + "px"), y && (p = parseFloat(f) + c + g, h = parseFloat(u) + c + g)), a = _e(P, b[l], f + " " + u, p + " " + h, !1, "0px", a);
        }

        return a;
      },
      prefix: !0,
      formatter: he("0px 0px 0px 0px", !1, !0)
    }), me("backgroundPosition", {
      defaultValue: "0 0",
      parser: function parser(t, e, i, r, n, a) {
        var o,
            l,
            h,
            u,
            f,
            p,
            _ = "background-position",
            d = s || q(t, null),
            m = this.format((d ? c ? d.getPropertyValue(_ + "-x") + " " + d.getPropertyValue(_ + "-y") : d.getPropertyValue(_) : t.currentStyle.backgroundPositionX + " " + t.currentStyle.backgroundPositionY) || "0 0"),
            g = this.format(e);

        if (-1 !== m.indexOf("%") != (-1 !== g.indexOf("%")) && (p = H(t, "backgroundImage").replace(k, ""), p && "none" !== p)) {
          for (o = m.split(" "), l = g.split(" "), I.setAttribute("src", p), h = 2; --h > -1;) {
            m = o[h], u = -1 !== m.indexOf("%"), u !== (-1 !== l[h].indexOf("%")) && (f = 0 === h ? t.offsetWidth - I.width : t.offsetHeight - I.height, o[h] = u ? parseFloat(m) / 100 * f + "px" : 100 * (parseFloat(m) / f) + "%");
          }

          m = o.join(" ");
        }

        return this.parseComplex(t.style, m, g, n, a);
      },
      formatter: ee
    }), me("backgroundSize", {
      defaultValue: "0 0",
      formatter: ee
    }), me("perspective", {
      defaultValue: "0px",
      prefix: !0
    }), me("perspectiveOrigin", {
      defaultValue: "50% 50%",
      prefix: !0
    }), me("transformStyle", {
      prefix: !0
    }), me("backfaceVisibility", {
      prefix: !0
    }), me("userSelect", {
      prefix: !0
    }), me("margin", {
      parser: ue("marginTop,marginRight,marginBottom,marginLeft")
    }), me("padding", {
      parser: ue("paddingTop,paddingRight,paddingBottom,paddingLeft")
    }), me("clip", {
      defaultValue: "rect(0px,0px,0px,0px)",
      parser: function parser(t, e, i, r, n, a) {
        var o, l, h;
        return 9 > c ? (l = t.currentStyle, h = 8 > c ? " " : ",", o = "rect(" + l.clipTop + h + l.clipRight + h + l.clipBottom + h + l.clipLeft + ")", e = this.format(e).split(",").join(h)) : (o = this.format(H(t, this.p, s, !1, this.dflt)), e = this.format(e)), this.parseComplex(t.style, o, e, n, a);
      }
    }), me("textShadow", {
      defaultValue: "0px 0px 0px #999",
      color: !0,
      multi: !0
    }), me("autoRound,strictUnits", {
      parser: function parser(t, e, i, r, s) {
        return s;
      }
    }), me("border", {
      defaultValue: "0px solid #000",
      parser: function parser(t, e, i, r, n, a) {
        return this.parseComplex(t.style, this.format(H(t, "borderTopWidth", s, !1, "0px") + " " + H(t, "borderTopStyle", s, !1, "solid") + " " + H(t, "borderTopColor", s, !1, "#000")), this.format(e), n, a);
      },
      color: !0,
      formatter: function formatter(t) {
        var e = t.split(" ");
        return e[0] + " " + (e[1] || "solid") + " " + (t.match(le) || ["#000"])[0];
      }
    }), me("borderWidth", {
      parser: ue("borderTopWidth,borderRightWidth,borderBottomWidth,borderLeftWidth")
    }), me("float,cssFloat,styleFloat", {
      parser: function parser(t, e, i, r, s) {
        var n = t.style,
            a = "cssFloat" in n ? "cssFloat" : "styleFloat";
        return new pe(n, a, 0, 0, s, -1, i, !1, 0, n[a], e);
      }
    });

    var Ce = function Ce(t) {
      var e,
          i = this.t,
          r = i.filter || H(this.data, "filter"),
          s = 0 | this.s + this.c * t;
      100 === s && (-1 === r.indexOf("atrix(") && -1 === r.indexOf("radient(") && -1 === r.indexOf("oader(") ? (i.removeAttribute("filter"), e = !H(this.data, "filter")) : (i.filter = r.replace(w, ""), e = !0)), e || (this.xn1 && (i.filter = r = r || "alpha(opacity=" + s + ")"), -1 === r.indexOf("pacity") ? 0 === s && this.xn1 || (i.filter = r + " alpha(opacity=" + s + ")") : i.filter = r.replace(T, "opacity=" + s));
    };

    me("opacity,alpha,autoAlpha", {
      defaultValue: "1",
      parser: function parser(t, e, i, r, n, a) {
        var o = parseFloat(H(t, "opacity", s, !1, "1")),
            l = t.style,
            h = "autoAlpha" === i;
        return "string" == typeof e && "=" === e.charAt(1) && (e = ("-" === e.charAt(0) ? -1 : 1) * parseFloat(e.substr(2)) + o), h && 1 === o && "hidden" === H(t, "visibility", s) && 0 !== e && (o = 0), Y ? n = new pe(l, "opacity", o, e - o, n) : (n = new pe(l, "opacity", 100 * o, 100 * (e - o), n), n.xn1 = h ? 1 : 0, l.zoom = 1, n.type = 2, n.b = "alpha(opacity=" + n.s + ")", n.e = "alpha(opacity=" + (n.s + n.c) + ")", n.data = t, n.plugin = a, n.setRatio = Ce), h && (n = new pe(l, "visibility", 0, 0, n, -1, null, !1, 0, 0 !== o ? "inherit" : "hidden", 0 === e ? "hidden" : "inherit"), n.xs0 = "inherit", r._overwriteProps.push(n.n), r._overwriteProps.push(i)), n;
      }
    });

    var Ae = function Ae(t, e) {
      e && (t.removeProperty ? ("ms" === e.substr(0, 2) && (e = "M" + e.substr(1)), t.removeProperty(e.replace(P, "-$1").toLowerCase())) : t.removeAttribute(e));
    },
        Oe = function Oe(t) {
      if (this.t._gsClassPT = this, 1 === t || 0 === t) {
        this.t.setAttribute("class", 0 === t ? this.b : this.e);

        for (var e = this.data, i = this.t.style; e;) {
          e.v ? i[e.p] = e.v : Ae(i, e.p), e = e._next;
        }

        1 === t && this.t._gsClassPT === this && (this.t._gsClassPT = null);
      } else this.t.getAttribute("class") !== this.e && this.t.setAttribute("class", this.e);
    };

    me("className", {
      parser: function parser(t, e, r, n, a, o, l) {
        var h,
            u,
            f,
            p,
            _,
            c = t.getAttribute("class") || "",
            d = t.style.cssText;

        if (a = n._classNamePT = new pe(t, r, 0, 0, a, 2), a.setRatio = Oe, a.pr = -11, i = !0, a.b = c, u = Z(t, s), f = t._gsClassPT) {
          for (p = {}, _ = f.data; _;) {
            p[_.p] = 1, _ = _._next;
          }

          f.setRatio(1);
        }

        return t._gsClassPT = a, a.e = "=" !== e.charAt(1) ? e : c.replace(RegExp("\\s*\\b" + e.substr(2) + "\\b"), "") + ("+" === e.charAt(0) ? " " + e.substr(2) : ""), n._tween._duration && (t.setAttribute("class", a.e), h = $(t, u, Z(t), l, p), t.setAttribute("class", c), a.data = h.firstMPT, t.style.cssText = d, a = a.xfirst = n.parse(t, h.difs, a, o)), a;
      }
    });

    var De = function De(t) {
      if ((1 === t || 0 === t) && this.data._totalTime === this.data._totalDuration && "isFromStart" !== this.data.data) {
        var e,
            i,
            r,
            s,
            n = this.t.style,
            a = o.transform.parse;
        if ("all" === this.e) n.cssText = "", s = !0;else for (e = this.e.split(","), r = e.length; --r > -1;) {
          i = e[r], o[i] && (o[i].parse === a ? s = !0 : i = "transformOrigin" === i ? xe : o[i].p), Ae(n, i);
        }
        s && (Ae(n, ye), this.t._gsTransform && delete this.t._gsTransform);
      }
    };

    for (me("clearProps", {
      parser: function parser(t, e, r, s, n) {
        return n = new pe(t, r, 0, 0, n, 2), n.setRatio = De, n.e = e, n.pr = -10, n.data = s._tween, i = !0, n;
      }
    }), l = "bezier,throwProps,physicsProps,physics2D".split(","), ce = l.length; ce--;) {
      ge(l[ce]);
    }

    l = a.prototype, l._firstPT = null, l._onInitTween = function (t, e, o) {
      if (!t.nodeType) return !1;
      this._target = t, this._tween = o, this._vars = e, h = e.autoRound, i = !1, r = e.suffixMap || a.suffixMap, s = q(t, ""), n = this._overwriteProps;
      var l,
          p,
          c,
          d,
          m,
          g,
          v,
          y,
          T,
          w = t.style;

      if (u && "" === w.zIndex && (l = H(t, "zIndex", s), ("auto" === l || "" === l) && this._addLazySet(w, "zIndex", 0)), "string" == typeof e && (d = w.cssText, l = Z(t, s), w.cssText = d + ";" + e, l = $(t, l, Z(t)).difs, !Y && x.test(e) && (l.opacity = parseFloat(RegExp.$1)), e = l, w.cssText = d), this._firstPT = p = this.parse(t, e, null), this._transformType) {
        for (T = 3 === this._transformType, ye ? f && (u = !0, "" === w.zIndex && (v = H(t, "zIndex", s), ("auto" === v || "" === v) && this._addLazySet(w, "zIndex", 0)), _ && this._addLazySet(w, "WebkitBackfaceVisibility", this._vars.WebkitBackfaceVisibility || (T ? "visible" : "hidden"))) : w.zoom = 1, c = p; c && c._next;) {
          c = c._next;
        }

        y = new pe(t, "transform", 0, 0, null, 2), this._linkCSSP(y, null, c), y.setRatio = T && we ? ke : ye ? Re : Se, y.data = this._transform || Pe(t, s, !0), n.pop();
      }

      if (i) {
        for (; p;) {
          for (g = p._next, c = d; c && c.pr > p.pr;) {
            c = c._next;
          }

          (p._prev = c ? c._prev : m) ? p._prev._next = p : d = p, (p._next = c) ? c._prev = p : m = p, p = g;
        }

        this._firstPT = d;
      }

      return !0;
    }, l.parse = function (t, e, i, n) {
      var a,
          l,
          u,
          f,
          p,
          _,
          c,
          d,
          m,
          g,
          v = t.style;

      for (a in e) {
        _ = e[a], l = o[a], l ? i = l.parse(t, _, a, this, i, n, e) : (p = H(t, a, s) + "", m = "string" == typeof _, "color" === a || "fill" === a || "stroke" === a || -1 !== a.indexOf("Color") || m && b.test(_) ? (m || (_ = oe(_), _ = (_.length > 3 ? "rgba(" : "rgb(") + _.join(",") + ")"), i = _e(v, a, p, _, !0, "transparent", i, 0, n)) : !m || -1 === _.indexOf(" ") && -1 === _.indexOf(",") ? (u = parseFloat(p), c = u || 0 === u ? p.substr((u + "").length) : "", ("" === p || "auto" === p) && ("width" === a || "height" === a ? (u = te(t, a, s), c = "px") : "left" === a || "top" === a ? (u = G(t, a, s), c = "px") : (u = "opacity" !== a ? 0 : 1, c = "")), g = m && "=" === _.charAt(1), g ? (f = parseInt(_.charAt(0) + "1", 10), _ = _.substr(2), f *= parseFloat(_), d = _.replace(y, "")) : (f = parseFloat(_), d = m ? _.substr((f + "").length) || "" : ""), "" === d && (d = a in r ? r[a] : c), _ = f || 0 === f ? (g ? f + u : f) + d : e[a], c !== d && "" !== d && (f || 0 === f) && u && (u = Q(t, a, u, c), "%" === d ? (u /= Q(t, a, 100, "%") / 100, e.strictUnits !== !0 && (p = u + "%")) : "em" === d ? u /= Q(t, a, 1, "em") : "px" !== d && (f = Q(t, a, f, d), d = "px"), g && (f || 0 === f) && (_ = f + u + d)), g && (f += u), !u && 0 !== u || !f && 0 !== f ? void 0 !== v[a] && (_ || "NaN" != _ + "" && null != _) ? (i = new pe(v, a, f || u || 0, 0, i, -1, a, !1, 0, p, _), i.xs0 = "none" !== _ || "display" !== a && -1 === a.indexOf("Style") ? _ : p) : U("invalid " + a + " tween value: " + e[a]) : (i = new pe(v, a, u, f - u, i, 0, a, h !== !1 && ("px" === d || "zIndex" === a), 0, p, _), i.xs0 = d)) : i = _e(v, a, p, _, !0, null, i, 0, n)), n && i && !i.plugin && (i.plugin = n);
      }

      return i;
    }, l.setRatio = function (t) {
      var e,
          i,
          r,
          s = this._firstPT,
          n = 1e-6;
      if (1 !== t || this._tween._time !== this._tween._duration && 0 !== this._tween._time) {
        if (t || this._tween._time !== this._tween._duration && 0 !== this._tween._time || this._tween._rawPrevTime === -1e-6) for (; s;) {
          if (e = s.c * t + s.s, s.r ? e = Math.round(e) : n > e && e > -n && (e = 0), s.type) {
            if (1 === s.type) {
              if (r = s.l, 2 === r) s.t[s.p] = s.xs0 + e + s.xs1 + s.xn1 + s.xs2;else if (3 === r) s.t[s.p] = s.xs0 + e + s.xs1 + s.xn1 + s.xs2 + s.xn2 + s.xs3;else if (4 === r) s.t[s.p] = s.xs0 + e + s.xs1 + s.xn1 + s.xs2 + s.xn2 + s.xs3 + s.xn3 + s.xs4;else if (5 === r) s.t[s.p] = s.xs0 + e + s.xs1 + s.xn1 + s.xs2 + s.xn2 + s.xs3 + s.xn3 + s.xs4 + s.xn4 + s.xs5;else {
                for (i = s.xs0 + e + s.xs1, r = 1; s.l > r; r++) {
                  i += s["xn" + r] + s["xs" + (r + 1)];
                }

                s.t[s.p] = i;
              }
            } else -1 === s.type ? s.t[s.p] = s.xs0 : s.setRatio && s.setRatio(t);
          } else s.t[s.p] = e + s.xs0;
          s = s._next;
        } else for (; s;) {
          2 !== s.type ? s.t[s.p] = s.b : s.setRatio(t), s = s._next;
        }
      } else for (; s;) {
        2 !== s.type ? s.t[s.p] = s.e : s.setRatio(t), s = s._next;
      }
    }, l._enableTransforms = function (t) {
      this._transformType = t || 3 === this._transformType ? 3 : 2, this._transform = this._transform || Pe(this._target, s, !0);
    };

    var Me = function Me() {
      this.t[this.p] = this.e, this.data._linkCSSP(this, this._next, null, !0);
    };

    l._addLazySet = function (t, e, i) {
      var r = this._firstPT = new pe(t, e, 0, 0, this._firstPT, 2);
      r.e = i, r.setRatio = Me, r.data = this;
    }, l._linkCSSP = function (t, e, i, r) {
      return t && (e && (e._prev = t), t._next && (t._next._prev = t._prev), t._prev ? t._prev._next = t._next : this._firstPT === t && (this._firstPT = t._next, r = !0), i ? i._next = t : r || null !== this._firstPT || (this._firstPT = t), t._next = e, t._prev = i), t;
    }, l._kill = function (e) {
      var i,
          r,
          s,
          n = e;

      if (e.autoAlpha || e.alpha) {
        n = {};

        for (r in e) {
          n[r] = e[r];
        }

        n.opacity = 1, n.autoAlpha && (n.visibility = 1);
      }

      return e.className && (i = this._classNamePT) && (s = i.xfirst, s && s._prev ? this._linkCSSP(s._prev, i._next, s._prev._prev) : s === this._firstPT && (this._firstPT = i._next), i._next && this._linkCSSP(i._next, i._next._next, s._prev), this._classNamePT = null), t.prototype._kill.call(this, n);
    };

    var Le = function Le(t, e, i) {
      var r, s, n, a;
      if (t.slice) for (s = t.length; --s > -1;) {
        Le(t[s], e, i);
      } else for (r = t.childNodes, s = r.length; --s > -1;) {
        n = r[s], a = n.type, n.style && (e.push(Z(n)), i && i.push(n)), 1 !== a && 9 !== a && 11 !== a || !n.childNodes.length || Le(n, e, i);
      }
    };

    return a.cascadeTo = function (t, i, r) {
      var s,
          n,
          a,
          o = e.to(t, i, r),
          l = [o],
          h = [],
          u = [],
          f = [],
          p = e._internals.reservedProps;

      for (t = o._targets || o.target, Le(t, h, f), o.render(i, !0), Le(t, u), o.render(0, !0), o._enabled(!0), s = f.length; --s > -1;) {
        if (n = $(f[s], h[s], u[s]), n.firstMPT) {
          n = n.difs;

          for (a in r) {
            p[a] && (n[a] = r[a]);
          }

          l.push(e.to(f[s], i, n));
        }
      }

      return l;
    }, t.activate([a]), a;
  }, !0);
}), _gsScope._gsDefine && _gsScope._gsQueue.pop()(), function (t) {
  "use strict";

  var e = function e() {
    return (_gsScope.GreenSockGlobals || _gsScope)[t];
  };

  "function" == typeof define && define.amd ? define(["TweenLite"], e) : "undefined" != typeof module && module.exports && (require("../TweenLite.js"), module.exports = e());
}("CSSPlugin");
//# sourceMappingURL=cssPlugin.js.map
