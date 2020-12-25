"use strict";

function _typeof(obj) { "@babel/helpers - typeof"; if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

/*!
 * VERSION: 1.18.4
 * DATE: 2016-04-26
 * UPDATES AND DOCS AT: http://greensock.com
 *
 * @license Copyright (c) 2008-2016, GreenSock. All rights reserved.
 * This work is subject to the terms at http://greensock.com/standard-license or for
 * Club GreenSock members, the software agreement that was issued with your membership.
 * 
 * @author: Jack Doyle, jack@greensock.com
 */
!function (a, b) {
  "use strict";

  var c = a.GreenSockGlobals = a.GreenSockGlobals || a;

  if (!c.TweenLite) {
    var d,
        e,
        f,
        g,
        h,
        i = function i(a) {
      var b,
          d = a.split("."),
          e = c;

      for (b = 0; b < d.length; b++) {
        e[d[b]] = e = e[d[b]] || {};
      }

      return e;
    },
        j = i("com.greensock"),
        k = 1e-10,
        l = function l(a) {
      var b,
          c = [],
          d = a.length;

      for (b = 0; b !== d; c.push(a[b++])) {
        ;
      }

      return c;
    },
        m = function m() {},
        n = function () {
      var a = Object.prototype.toString,
          b = a.call([]);
      return function (c) {
        return null != c && (c instanceof Array || "object" == _typeof(c) && !!c.push && a.call(c) === b);
      };
    }(),
        o = {},
        p = function p(d, e, f, g) {
      this.sc = o[d] ? o[d].sc : [], o[d] = this, this.gsClass = null, this.func = f;
      var h = [];
      this.check = function (j) {
        for (var k, l, m, n, q, r = e.length, s = r; --r > -1;) {
          (k = o[e[r]] || new p(e[r], [])).gsClass ? (h[r] = k.gsClass, s--) : j && k.sc.push(this);
        }

        if (0 === s && f) for (l = ("com.greensock." + d).split("."), m = l.pop(), n = i(l.join("."))[m] = this.gsClass = f.apply(f, h), g && (c[m] = n, q = "undefined" != typeof module && module.exports, !q && "function" == typeof define && define.amd ? define((a.GreenSockAMDPath ? a.GreenSockAMDPath + "/" : "") + d.split(".").pop(), [], function () {
          return n;
        }) : d === b && q && (module.exports = n)), r = 0; r < this.sc.length; r++) {
          this.sc[r].check();
        }
      }, this.check(!0);
    },
        q = a._gsDefine = function (a, b, c, d) {
      return new p(a, b, c, d);
    },
        r = j._class = function (a, b, c) {
      return b = b || function () {}, q(a, [], function () {
        return b;
      }, c), b;
    };

    q.globals = c;

    var s = [0, 0, 1, 1],
        t = [],
        u = r("easing.Ease", function (a, b, c, d) {
      this._func = a, this._type = c || 0, this._power = d || 0, this._params = b ? s.concat(b) : s;
    }, !0),
        v = u.map = {},
        w = u.register = function (a, b, c, d) {
      for (var e, f, g, h, i = b.split(","), k = i.length, l = (c || "easeIn,easeOut,easeInOut").split(","); --k > -1;) {
        for (f = i[k], e = d ? r("easing." + f, null, !0) : j.easing[f] || {}, g = l.length; --g > -1;) {
          h = l[g], v[f + "." + h] = v[h + f] = e[h] = a.getRatio ? a : a[h] || new a();
        }
      }
    };

    for (f = u.prototype, f._calcEnd = !1, f.getRatio = function (a) {
      if (this._func) return this._params[0] = a, this._func.apply(null, this._params);
      var b = this._type,
          c = this._power,
          d = 1 === b ? 1 - a : 2 === b ? a : .5 > a ? 2 * a : 2 * (1 - a);
      return 1 === c ? d *= d : 2 === c ? d *= d * d : 3 === c ? d *= d * d * d : 4 === c && (d *= d * d * d * d), 1 === b ? 1 - d : 2 === b ? d : .5 > a ? d / 2 : 1 - d / 2;
    }, d = ["Linear", "Quad", "Cubic", "Quart", "Quint,Strong"], e = d.length; --e > -1;) {
      f = d[e] + ",Power" + e, w(new u(null, null, 1, e), f, "easeOut", !0), w(new u(null, null, 2, e), f, "easeIn" + (0 === e ? ",easeNone" : "")), w(new u(null, null, 3, e), f, "easeInOut");
    }

    v.linear = j.easing.Linear.easeIn, v.swing = j.easing.Quad.easeInOut;
    var x = r("events.EventDispatcher", function (a) {
      this._listeners = {}, this._eventTarget = a || this;
    });
    f = x.prototype, f.addEventListener = function (a, b, c, d, e) {
      e = e || 0;
      var f,
          i,
          j = this._listeners[a],
          k = 0;

      for (null == j && (this._listeners[a] = j = []), i = j.length; --i > -1;) {
        f = j[i], f.c === b && f.s === c ? j.splice(i, 1) : 0 === k && f.pr < e && (k = i + 1);
      }

      j.splice(k, 0, {
        c: b,
        s: c,
        up: d,
        pr: e
      }), this !== g || h || g.wake();
    }, f.removeEventListener = function (a, b) {
      var c,
          d = this._listeners[a];
      if (d) for (c = d.length; --c > -1;) {
        if (d[c].c === b) return void d.splice(c, 1);
      }
    }, f.dispatchEvent = function (a) {
      var b,
          c,
          d,
          e = this._listeners[a];
      if (e) for (b = e.length, c = this._eventTarget; --b > -1;) {
        d = e[b], d && (d.up ? d.c.call(d.s || c, {
          type: a,
          target: c
        }) : d.c.call(d.s || c));
      }
    };

    var y = a.requestAnimationFrame,
        z = a.cancelAnimationFrame,
        A = Date.now || function () {
      return new Date().getTime();
    },
        B = A();

    for (d = ["ms", "moz", "webkit", "o"], e = d.length; --e > -1 && !y;) {
      y = a[d[e] + "RequestAnimationFrame"], z = a[d[e] + "CancelAnimationFrame"] || a[d[e] + "CancelRequestAnimationFrame"];
    }

    r("Ticker", function (a, b) {
      var c,
          d,
          e,
          f,
          i,
          j = this,
          l = A(),
          n = b !== !1 && y ? "auto" : !1,
          o = 500,
          p = 33,
          q = "tick",
          r = function r(a) {
        var b,
            g,
            h = A() - B;
        h > o && (l += h - p), B += h, j.time = (B - l) / 1e3, b = j.time - i, (!c || b > 0 || a === !0) && (j.frame++, i += b + (b >= f ? .004 : f - b), g = !0), a !== !0 && (e = d(r)), g && j.dispatchEvent(q);
      };

      x.call(j), j.time = j.frame = 0, j.tick = function () {
        r(!0);
      }, j.lagSmoothing = function (a, b) {
        o = a || 1 / k, p = Math.min(b, o, 0);
      }, j.sleep = function () {
        null != e && (n && z ? z(e) : clearTimeout(e), d = m, e = null, j === g && (h = !1));
      }, j.wake = function (a) {
        null !== e ? j.sleep() : a ? l += -B + (B = A()) : j.frame > 10 && (B = A() - o + 5), d = 0 === c ? m : n && y ? y : function (a) {
          return setTimeout(a, 1e3 * (i - j.time) + 1 | 0);
        }, j === g && (h = !0), r(2);
      }, j.fps = function (a) {
        return arguments.length ? (c = a, f = 1 / (c || 60), i = this.time + f, void j.wake()) : c;
      }, j.useRAF = function (a) {
        return arguments.length ? (j.sleep(), n = a, void j.fps(c)) : n;
      }, j.fps(a), setTimeout(function () {
        "auto" === n && j.frame < 5 && "hidden" !== document.visibilityState && j.useRAF(!1);
      }, 1500);
    }), f = j.Ticker.prototype = new j.events.EventDispatcher(), f.constructor = j.Ticker;
    var C = r("core.Animation", function (a, b) {
      if (this.vars = b = b || {}, this._duration = this._totalDuration = a || 0, this._delay = Number(b.delay) || 0, this._timeScale = 1, this._active = b.immediateRender === !0, this.data = b.data, this._reversed = b.reversed === !0, V) {
        h || g.wake();
        var c = this.vars.useFrames ? U : V;
        c.add(this, c._time), this.vars.paused && this.paused(!0);
      }
    });
    g = C.ticker = new j.Ticker(), f = C.prototype, f._dirty = f._gc = f._initted = f._paused = !1, f._totalTime = f._time = 0, f._rawPrevTime = -1, f._next = f._last = f._onUpdate = f._timeline = f.timeline = null, f._paused = !1;

    var D = function D() {
      h && A() - B > 2e3 && g.wake(), setTimeout(D, 2e3);
    };

    D(), f.play = function (a, b) {
      return null != a && this.seek(a, b), this.reversed(!1).paused(!1);
    }, f.pause = function (a, b) {
      return null != a && this.seek(a, b), this.paused(!0);
    }, f.resume = function (a, b) {
      return null != a && this.seek(a, b), this.paused(!1);
    }, f.seek = function (a, b) {
      return this.totalTime(Number(a), b !== !1);
    }, f.restart = function (a, b) {
      return this.reversed(!1).paused(!1).totalTime(a ? -this._delay : 0, b !== !1, !0);
    }, f.reverse = function (a, b) {
      return null != a && this.seek(a || this.totalDuration(), b), this.reversed(!0).paused(!1);
    }, f.render = function (a, b, c) {}, f.invalidate = function () {
      return this._time = this._totalTime = 0, this._initted = this._gc = !1, this._rawPrevTime = -1, (this._gc || !this.timeline) && this._enabled(!0), this;
    }, f.isActive = function () {
      var a,
          b = this._timeline,
          c = this._startTime;
      return !b || !this._gc && !this._paused && b.isActive() && (a = b.rawTime()) >= c && a < c + this.totalDuration() / this._timeScale;
    }, f._enabled = function (a, b) {
      return h || g.wake(), this._gc = !a, this._active = this.isActive(), b !== !0 && (a && !this.timeline ? this._timeline.add(this, this._startTime - this._delay) : !a && this.timeline && this._timeline._remove(this, !0)), !1;
    }, f._kill = function (a, b) {
      return this._enabled(!1, !1);
    }, f.kill = function (a, b) {
      return this._kill(a, b), this;
    }, f._uncache = function (a) {
      for (var b = a ? this : this.timeline; b;) {
        b._dirty = !0, b = b.timeline;
      }

      return this;
    }, f._swapSelfInParams = function (a) {
      for (var b = a.length, c = a.concat(); --b > -1;) {
        "{self}" === a[b] && (c[b] = this);
      }

      return c;
    }, f._callback = function (a) {
      var b = this.vars;
      b[a].apply(b[a + "Scope"] || b.callbackScope || this, b[a + "Params"] || t);
    }, f.eventCallback = function (a, b, c, d) {
      if ("on" === (a || "").substr(0, 2)) {
        var e = this.vars;
        if (1 === arguments.length) return e[a];
        null == b ? delete e[a] : (e[a] = b, e[a + "Params"] = n(c) && -1 !== c.join("").indexOf("{self}") ? this._swapSelfInParams(c) : c, e[a + "Scope"] = d), "onUpdate" === a && (this._onUpdate = b);
      }

      return this;
    }, f.delay = function (a) {
      return arguments.length ? (this._timeline.smoothChildTiming && this.startTime(this._startTime + a - this._delay), this._delay = a, this) : this._delay;
    }, f.duration = function (a) {
      return arguments.length ? (this._duration = this._totalDuration = a, this._uncache(!0), this._timeline.smoothChildTiming && this._time > 0 && this._time < this._duration && 0 !== a && this.totalTime(this._totalTime * (a / this._duration), !0), this) : (this._dirty = !1, this._duration);
    }, f.totalDuration = function (a) {
      return this._dirty = !1, arguments.length ? this.duration(a) : this._totalDuration;
    }, f.time = function (a, b) {
      return arguments.length ? (this._dirty && this.totalDuration(), this.totalTime(a > this._duration ? this._duration : a, b)) : this._time;
    }, f.totalTime = function (a, b, c) {
      if (h || g.wake(), !arguments.length) return this._totalTime;

      if (this._timeline) {
        if (0 > a && !c && (a += this.totalDuration()), this._timeline.smoothChildTiming) {
          this._dirty && this.totalDuration();
          var d = this._totalDuration,
              e = this._timeline;
          if (a > d && !c && (a = d), this._startTime = (this._paused ? this._pauseTime : e._time) - (this._reversed ? d - a : a) / this._timeScale, e._dirty || this._uncache(!1), e._timeline) for (; e._timeline;) {
            e._timeline._time !== (e._startTime + e._totalTime) / e._timeScale && e.totalTime(e._totalTime, !0), e = e._timeline;
          }
        }

        this._gc && this._enabled(!0, !1), (this._totalTime !== a || 0 === this._duration) && (I.length && X(), this.render(a, b, !1), I.length && X());
      }

      return this;
    }, f.progress = f.totalProgress = function (a, b) {
      var c = this.duration();
      return arguments.length ? this.totalTime(c * a, b) : c ? this._time / c : this.ratio;
    }, f.startTime = function (a) {
      return arguments.length ? (a !== this._startTime && (this._startTime = a, this.timeline && this.timeline._sortChildren && this.timeline.add(this, a - this._delay)), this) : this._startTime;
    }, f.endTime = function (a) {
      return this._startTime + (0 != a ? this.totalDuration() : this.duration()) / this._timeScale;
    }, f.timeScale = function (a) {
      if (!arguments.length) return this._timeScale;

      if (a = a || k, this._timeline && this._timeline.smoothChildTiming) {
        var b = this._pauseTime,
            c = b || 0 === b ? b : this._timeline.totalTime();
        this._startTime = c - (c - this._startTime) * this._timeScale / a;
      }

      return this._timeScale = a, this._uncache(!1);
    }, f.reversed = function (a) {
      return arguments.length ? (a != this._reversed && (this._reversed = a, this.totalTime(this._timeline && !this._timeline.smoothChildTiming ? this.totalDuration() - this._totalTime : this._totalTime, !0)), this) : this._reversed;
    }, f.paused = function (a) {
      if (!arguments.length) return this._paused;
      var b,
          c,
          d = this._timeline;
      return a != this._paused && d && (h || a || g.wake(), b = d.rawTime(), c = b - this._pauseTime, !a && d.smoothChildTiming && (this._startTime += c, this._uncache(!1)), this._pauseTime = a ? b : null, this._paused = a, this._active = this.isActive(), !a && 0 !== c && this._initted && this.duration() && (b = d.smoothChildTiming ? this._totalTime : (b - this._startTime) / this._timeScale, this.render(b, b === this._totalTime, !0))), this._gc && !a && this._enabled(!0, !1), this;
    };
    var E = r("core.SimpleTimeline", function (a) {
      C.call(this, 0, a), this.autoRemoveChildren = this.smoothChildTiming = !0;
    });
    f = E.prototype = new C(), f.constructor = E, f.kill()._gc = !1, f._first = f._last = f._recent = null, f._sortChildren = !1, f.add = f.insert = function (a, b, c, d) {
      var e, f;
      if (a._startTime = Number(b || 0) + a._delay, a._paused && this !== a._timeline && (a._pauseTime = a._startTime + (this.rawTime() - a._startTime) / a._timeScale), a.timeline && a.timeline._remove(a, !0), a.timeline = a._timeline = this, a._gc && a._enabled(!0, !0), e = this._last, this._sortChildren) for (f = a._startTime; e && e._startTime > f;) {
        e = e._prev;
      }
      return e ? (a._next = e._next, e._next = a) : (a._next = this._first, this._first = a), a._next ? a._next._prev = a : this._last = a, a._prev = e, this._recent = a, this._timeline && this._uncache(!0), this;
    }, f._remove = function (a, b) {
      return a.timeline === this && (b || a._enabled(!1, !0), a._prev ? a._prev._next = a._next : this._first === a && (this._first = a._next), a._next ? a._next._prev = a._prev : this._last === a && (this._last = a._prev), a._next = a._prev = a.timeline = null, a === this._recent && (this._recent = this._last), this._timeline && this._uncache(!0)), this;
    }, f.render = function (a, b, c) {
      var d,
          e = this._first;

      for (this._totalTime = this._time = this._rawPrevTime = a; e;) {
        d = e._next, (e._active || a >= e._startTime && !e._paused) && (e._reversed ? e.render((e._dirty ? e.totalDuration() : e._totalDuration) - (a - e._startTime) * e._timeScale, b, c) : e.render((a - e._startTime) * e._timeScale, b, c)), e = d;
      }
    }, f.rawTime = function () {
      return h || g.wake(), this._totalTime;
    };

    var F = r("TweenLite", function (b, c, d) {
      if (C.call(this, c, d), this.render = F.prototype.render, null == b) throw "Cannot tween a null target.";
      this.target = b = "string" != typeof b ? b : F.selector(b) || b;
      var e,
          f,
          g,
          h = b.jquery || b.length && b !== a && b[0] && (b[0] === a || b[0].nodeType && b[0].style && !b.nodeType),
          i = this.vars.overwrite;
      if (this._overwrite = i = null == i ? T[F.defaultOverwrite] : "number" == typeof i ? i >> 0 : T[i], (h || b instanceof Array || b.push && n(b)) && "number" != typeof b[0]) for (this._targets = g = l(b), this._propLookup = [], this._siblings = [], e = 0; e < g.length; e++) {
        f = g[e], f ? "string" != typeof f ? f.length && f !== a && f[0] && (f[0] === a || f[0].nodeType && f[0].style && !f.nodeType) ? (g.splice(e--, 1), this._targets = g = g.concat(l(f))) : (this._siblings[e] = Y(f, this, !1), 1 === i && this._siblings[e].length > 1 && $(f, this, null, 1, this._siblings[e])) : (f = g[e--] = F.selector(f), "string" == typeof f && g.splice(e + 1, 1)) : g.splice(e--, 1);
      } else this._propLookup = {}, this._siblings = Y(b, this, !1), 1 === i && this._siblings.length > 1 && $(b, this, null, 1, this._siblings);
      (this.vars.immediateRender || 0 === c && 0 === this._delay && this.vars.immediateRender !== !1) && (this._time = -k, this.render(Math.min(0, -this._delay)));
    }, !0),
        G = function G(b) {
      return b && b.length && b !== a && b[0] && (b[0] === a || b[0].nodeType && b[0].style && !b.nodeType);
    },
        H = function H(a, b) {
      var c,
          d = {};

      for (c in a) {
        S[c] || c in b && "transform" !== c && "x" !== c && "y" !== c && "width" !== c && "height" !== c && "className" !== c && "border" !== c || !(!P[c] || P[c] && P[c]._autoCSS) || (d[c] = a[c], delete a[c]);
      }

      a.css = d;
    };

    f = F.prototype = new C(), f.constructor = F, f.kill()._gc = !1, f.ratio = 0, f._firstPT = f._targets = f._overwrittenProps = f._startAt = null, f._notifyPluginsOfEnabled = f._lazy = !1, F.version = "1.18.4", F.defaultEase = f._ease = new u(null, null, 1, 1), F.defaultOverwrite = "auto", F.ticker = g, F.autoSleep = 120, F.lagSmoothing = function (a, b) {
      g.lagSmoothing(a, b);
    }, F.selector = a.$ || a.jQuery || function (b) {
      var c = a.$ || a.jQuery;
      return c ? (F.selector = c, c(b)) : "undefined" == typeof document ? b : document.querySelectorAll ? document.querySelectorAll(b) : document.getElementById("#" === b.charAt(0) ? b.substr(1) : b);
    };

    var I = [],
        J = {},
        K = /(?:(-|-=|\+=)?\d*\.?\d*(?:e[\-+]?\d+)?)[0-9]/gi,
        L = function L(a) {
      for (var b, c = this._firstPT, d = 1e-6; c;) {
        b = c.blob ? a ? this.join("") : this.start : c.c * a + c.s, c.r ? b = Math.round(b) : d > b && b > -d && (b = 0), c.f ? c.fp ? c.t[c.p](c.fp, b) : c.t[c.p](b) : c.t[c.p] = b, c = c._next;
      }
    },
        M = function M(a, b, c, d) {
      var e,
          f,
          g,
          h,
          i,
          j,
          k,
          l = [a, b],
          m = 0,
          n = "",
          o = 0;

      for (l.start = a, c && (c(l), a = l[0], b = l[1]), l.length = 0, e = a.match(K) || [], f = b.match(K) || [], d && (d._next = null, d.blob = 1, l._firstPT = d), i = f.length, h = 0; i > h; h++) {
        k = f[h], j = b.substr(m, b.indexOf(k, m) - m), n += j || !h ? j : ",", m += j.length, o ? o = (o + 1) % 5 : "rgba(" === j.substr(-5) && (o = 1), k === e[h] || e.length <= h ? n += k : (n && (l.push(n), n = ""), g = parseFloat(e[h]), l.push(g), l._firstPT = {
          _next: l._firstPT,
          t: l,
          p: l.length - 1,
          s: g,
          c: ("=" === k.charAt(1) ? parseInt(k.charAt(0) + "1", 10) * parseFloat(k.substr(2)) : parseFloat(k) - g) || 0,
          f: 0,
          r: o && 4 > o
        }), m += k.length;
      }

      return n += b.substr(m), n && l.push(n), l.setRatio = L, l;
    },
        N = function N(a, b, c, d, e, f, g, h) {
      var i,
          j,
          k = "get" === c ? a[b] : c,
          l = _typeof(a[b]),
          m = "string" == typeof d && "=" === d.charAt(1),
          n = {
        t: a,
        p: b,
        s: k,
        f: "function" === l,
        pg: 0,
        n: e || b,
        r: f,
        pr: 0,
        c: m ? parseInt(d.charAt(0) + "1", 10) * parseFloat(d.substr(2)) : parseFloat(d) - k || 0
      };

      return "number" !== l && ("function" === l && "get" === c && (j = b.indexOf("set") || "function" != typeof a["get" + b.substr(3)] ? b : "get" + b.substr(3), n.s = k = g ? a[j](g) : a[j]()), "string" == typeof k && (g || isNaN(k)) ? (n.fp = g, i = M(k, d, h || F.defaultStringFilter, n), n = {
        t: i,
        p: "setRatio",
        s: 0,
        c: 1,
        f: 2,
        pg: 0,
        n: e || b,
        pr: 0
      }) : m || (n.s = parseFloat(k), n.c = parseFloat(d) - n.s || 0)), n.c ? ((n._next = this._firstPT) && (n._next._prev = n), this._firstPT = n, n) : void 0;
    },
        O = F._internals = {
      isArray: n,
      isSelector: G,
      lazyTweens: I,
      blobDif: M
    },
        P = F._plugins = {},
        Q = O.tweenLookup = {},
        R = 0,
        S = O.reservedProps = {
      ease: 1,
      delay: 1,
      overwrite: 1,
      onComplete: 1,
      onCompleteParams: 1,
      onCompleteScope: 1,
      useFrames: 1,
      runBackwards: 1,
      startAt: 1,
      onUpdate: 1,
      onUpdateParams: 1,
      onUpdateScope: 1,
      onStart: 1,
      onStartParams: 1,
      onStartScope: 1,
      onReverseComplete: 1,
      onReverseCompleteParams: 1,
      onReverseCompleteScope: 1,
      onRepeat: 1,
      onRepeatParams: 1,
      onRepeatScope: 1,
      easeParams: 1,
      yoyo: 1,
      immediateRender: 1,
      repeat: 1,
      repeatDelay: 1,
      data: 1,
      paused: 1,
      reversed: 1,
      autoCSS: 1,
      lazy: 1,
      onOverwrite: 1,
      callbackScope: 1,
      stringFilter: 1
    },
        T = {
      none: 0,
      all: 1,
      auto: 2,
      concurrent: 3,
      allOnStart: 4,
      preexisting: 5,
      "true": 1,
      "false": 0
    },
        U = C._rootFramesTimeline = new E(),
        V = C._rootTimeline = new E(),
        W = 30,
        X = O.lazyRender = function () {
      var a,
          b = I.length;

      for (J = {}; --b > -1;) {
        a = I[b], a && a._lazy !== !1 && (a.render(a._lazy[0], a._lazy[1], !0), a._lazy = !1);
      }

      I.length = 0;
    };

    V._startTime = g.time, U._startTime = g.frame, V._active = U._active = !0, setTimeout(X, 1), C._updateRoot = F.render = function () {
      var a, b, c;

      if (I.length && X(), V.render((g.time - V._startTime) * V._timeScale, !1, !1), U.render((g.frame - U._startTime) * U._timeScale, !1, !1), I.length && X(), g.frame >= W) {
        W = g.frame + (parseInt(F.autoSleep, 10) || 120);

        for (c in Q) {
          for (b = Q[c].tweens, a = b.length; --a > -1;) {
            b[a]._gc && b.splice(a, 1);
          }

          0 === b.length && delete Q[c];
        }

        if (c = V._first, (!c || c._paused) && F.autoSleep && !U._first && 1 === g._listeners.tick.length) {
          for (; c && c._paused;) {
            c = c._next;
          }

          c || g.sleep();
        }
      }
    }, g.addEventListener("tick", C._updateRoot);

    var Y = function Y(a, b, c) {
      var d,
          e,
          f = a._gsTweenID;
      if (Q[f || (a._gsTweenID = f = "t" + R++)] || (Q[f] = {
        target: a,
        tweens: []
      }), b && (d = Q[f].tweens, d[e = d.length] = b, c)) for (; --e > -1;) {
        d[e] === b && d.splice(e, 1);
      }
      return Q[f].tweens;
    },
        Z = function Z(a, b, c, d) {
      var e,
          f,
          g = a.vars.onOverwrite;
      return g && (e = g(a, b, c, d)), g = F.onOverwrite, g && (f = g(a, b, c, d)), e !== !1 && f !== !1;
    },
        $ = function $(a, b, c, d, e) {
      var f, g, h, i;

      if (1 === d || d >= 4) {
        for (i = e.length, f = 0; i > f; f++) {
          if ((h = e[f]) !== b) h._gc || h._kill(null, a, b) && (g = !0);else if (5 === d) break;
        }

        return g;
      }

      var j,
          l = b._startTime + k,
          m = [],
          n = 0,
          o = 0 === b._duration;

      for (f = e.length; --f > -1;) {
        (h = e[f]) === b || h._gc || h._paused || (h._timeline !== b._timeline ? (j = j || _(b, 0, o), 0 === _(h, j, o) && (m[n++] = h)) : h._startTime <= l && h._startTime + h.totalDuration() / h._timeScale > l && ((o || !h._initted) && l - h._startTime <= 2e-10 || (m[n++] = h)));
      }

      for (f = n; --f > -1;) {
        if (h = m[f], 2 === d && h._kill(c, a, b) && (g = !0), 2 !== d || !h._firstPT && h._initted) {
          if (2 !== d && !Z(h, b)) continue;
          h._enabled(!1, !1) && (g = !0);
        }
      }

      return g;
    },
        _ = function _(a, b, c) {
      for (var d = a._timeline, e = d._timeScale, f = a._startTime; d._timeline;) {
        if (f += d._startTime, e *= d._timeScale, d._paused) return -100;
        d = d._timeline;
      }

      return f /= e, f > b ? f - b : c && f === b || !a._initted && 2 * k > f - b ? k : (f += a.totalDuration() / a._timeScale / e) > b + k ? 0 : f - b - k;
    };

    f._init = function () {
      var a,
          b,
          c,
          d,
          e,
          f = this.vars,
          g = this._overwrittenProps,
          h = this._duration,
          i = !!f.immediateRender,
          j = f.ease;

      if (f.startAt) {
        this._startAt && (this._startAt.render(-1, !0), this._startAt.kill()), e = {};

        for (d in f.startAt) {
          e[d] = f.startAt[d];
        }

        if (e.overwrite = !1, e.immediateRender = !0, e.lazy = i && f.lazy !== !1, e.startAt = e.delay = null, this._startAt = F.to(this.target, 0, e), i) if (this._time > 0) this._startAt = null;else if (0 !== h) return;
      } else if (f.runBackwards && 0 !== h) if (this._startAt) this._startAt.render(-1, !0), this._startAt.kill(), this._startAt = null;else {
        0 !== this._time && (i = !1), c = {};

        for (d in f) {
          S[d] && "autoCSS" !== d || (c[d] = f[d]);
        }

        if (c.overwrite = 0, c.data = "isFromStart", c.lazy = i && f.lazy !== !1, c.immediateRender = i, this._startAt = F.to(this.target, 0, c), i) {
          if (0 === this._time) return;
        } else this._startAt._init(), this._startAt._enabled(!1), this.vars.immediateRender && (this._startAt = null);
      }

      if (this._ease = j = j ? j instanceof u ? j : "function" == typeof j ? new u(j, f.easeParams) : v[j] || F.defaultEase : F.defaultEase, f.easeParams instanceof Array && j.config && (this._ease = j.config.apply(j, f.easeParams)), this._easeType = this._ease._type, this._easePower = this._ease._power, this._firstPT = null, this._targets) for (a = this._targets.length; --a > -1;) {
        this._initProps(this._targets[a], this._propLookup[a] = {}, this._siblings[a], g ? g[a] : null) && (b = !0);
      } else b = this._initProps(this.target, this._propLookup, this._siblings, g);
      if (b && F._onPluginEvent("_onInitAllProps", this), g && (this._firstPT || "function" != typeof this.target && this._enabled(!1, !1)), f.runBackwards) for (c = this._firstPT; c;) {
        c.s += c.c, c.c = -c.c, c = c._next;
      }
      this._onUpdate = f.onUpdate, this._initted = !0;
    }, f._initProps = function (b, c, d, e) {
      var f, g, h, i, j, k;
      if (null == b) return !1;
      J[b._gsTweenID] && X(), this.vars.css || b.style && b !== a && b.nodeType && P.css && this.vars.autoCSS !== !1 && H(this.vars, b);

      for (f in this.vars) {
        if (k = this.vars[f], S[f]) k && (k instanceof Array || k.push && n(k)) && -1 !== k.join("").indexOf("{self}") && (this.vars[f] = k = this._swapSelfInParams(k, this));else if (P[f] && (i = new P[f]())._onInitTween(b, this.vars[f], this)) {
          for (this._firstPT = j = {
            _next: this._firstPT,
            t: i,
            p: "setRatio",
            s: 0,
            c: 1,
            f: 1,
            n: f,
            pg: 1,
            pr: i._priority
          }, g = i._overwriteProps.length; --g > -1;) {
            c[i._overwriteProps[g]] = this._firstPT;
          }

          (i._priority || i._onInitAllProps) && (h = !0), (i._onDisable || i._onEnable) && (this._notifyPluginsOfEnabled = !0), j._next && (j._next._prev = j);
        } else c[f] = N.call(this, b, f, "get", k, f, 0, null, this.vars.stringFilter);
      }

      return e && this._kill(e, b) ? this._initProps(b, c, d, e) : this._overwrite > 1 && this._firstPT && d.length > 1 && $(b, this, c, this._overwrite, d) ? (this._kill(c, b), this._initProps(b, c, d, e)) : (this._firstPT && (this.vars.lazy !== !1 && this._duration || this.vars.lazy && !this._duration) && (J[b._gsTweenID] = !0), h);
    }, f.render = function (a, b, c) {
      var d,
          e,
          f,
          g,
          h = this._time,
          i = this._duration,
          j = this._rawPrevTime;
      if (a >= i - 1e-7) this._totalTime = this._time = i, this.ratio = this._ease._calcEnd ? this._ease.getRatio(1) : 1, this._reversed || (d = !0, e = "onComplete", c = c || this._timeline.autoRemoveChildren), 0 === i && (this._initted || !this.vars.lazy || c) && (this._startTime === this._timeline._duration && (a = 0), (0 > j || 0 >= a && a >= -1e-7 || j === k && "isPause" !== this.data) && j !== a && (c = !0, j > k && (e = "onReverseComplete")), this._rawPrevTime = g = !b || a || j === a ? a : k);else if (1e-7 > a) this._totalTime = this._time = 0, this.ratio = this._ease._calcEnd ? this._ease.getRatio(0) : 0, (0 !== h || 0 === i && j > 0) && (e = "onReverseComplete", d = this._reversed), 0 > a && (this._active = !1, 0 === i && (this._initted || !this.vars.lazy || c) && (j >= 0 && (j !== k || "isPause" !== this.data) && (c = !0), this._rawPrevTime = g = !b || a || j === a ? a : k)), this._initted || (c = !0);else if (this._totalTime = this._time = a, this._easeType) {
        var l = a / i,
            m = this._easeType,
            n = this._easePower;
        (1 === m || 3 === m && l >= .5) && (l = 1 - l), 3 === m && (l *= 2), 1 === n ? l *= l : 2 === n ? l *= l * l : 3 === n ? l *= l * l * l : 4 === n && (l *= l * l * l * l), 1 === m ? this.ratio = 1 - l : 2 === m ? this.ratio = l : .5 > a / i ? this.ratio = l / 2 : this.ratio = 1 - l / 2;
      } else this.ratio = this._ease.getRatio(a / i);

      if (this._time !== h || c) {
        if (!this._initted) {
          if (this._init(), !this._initted || this._gc) return;
          if (!c && this._firstPT && (this.vars.lazy !== !1 && this._duration || this.vars.lazy && !this._duration)) return this._time = this._totalTime = h, this._rawPrevTime = j, I.push(this), void (this._lazy = [a, b]);
          this._time && !d ? this.ratio = this._ease.getRatio(this._time / i) : d && this._ease._calcEnd && (this.ratio = this._ease.getRatio(0 === this._time ? 0 : 1));
        }

        for (this._lazy !== !1 && (this._lazy = !1), this._active || !this._paused && this._time !== h && a >= 0 && (this._active = !0), 0 === h && (this._startAt && (a >= 0 ? this._startAt.render(a, b, c) : e || (e = "_dummyGS")), this.vars.onStart && (0 !== this._time || 0 === i) && (b || this._callback("onStart"))), f = this._firstPT; f;) {
          f.f ? f.t[f.p](f.c * this.ratio + f.s) : f.t[f.p] = f.c * this.ratio + f.s, f = f._next;
        }

        this._onUpdate && (0 > a && this._startAt && a !== -1e-4 && this._startAt.render(a, b, c), b || (this._time !== h || d || c) && this._callback("onUpdate")), e && (!this._gc || c) && (0 > a && this._startAt && !this._onUpdate && a !== -1e-4 && this._startAt.render(a, b, c), d && (this._timeline.autoRemoveChildren && this._enabled(!1, !1), this._active = !1), !b && this.vars[e] && this._callback(e), 0 === i && this._rawPrevTime === k && g !== k && (this._rawPrevTime = 0));
      }
    }, f._kill = function (a, b, c) {
      if ("all" === a && (a = null), null == a && (null == b || b === this.target)) return this._lazy = !1, this._enabled(!1, !1);
      b = "string" != typeof b ? b || this._targets || this.target : F.selector(b) || b;
      var d,
          e,
          f,
          g,
          h,
          i,
          j,
          k,
          l,
          m = c && this._time && c._startTime === this._startTime && this._timeline === c._timeline;
      if ((n(b) || G(b)) && "number" != typeof b[0]) for (d = b.length; --d > -1;) {
        this._kill(a, b[d], c) && (i = !0);
      } else {
        if (this._targets) {
          for (d = this._targets.length; --d > -1;) {
            if (b === this._targets[d]) {
              h = this._propLookup[d] || {}, this._overwrittenProps = this._overwrittenProps || [], e = this._overwrittenProps[d] = a ? this._overwrittenProps[d] || {} : "all";
              break;
            }
          }
        } else {
          if (b !== this.target) return !1;
          h = this._propLookup, e = this._overwrittenProps = a ? this._overwrittenProps || {} : "all";
        }

        if (h) {
          if (j = a || h, k = a !== e && "all" !== e && a !== h && ("object" != _typeof(a) || !a._tempKill), c && (F.onOverwrite || this.vars.onOverwrite)) {
            for (f in j) {
              h[f] && (l || (l = []), l.push(f));
            }

            if ((l || !a) && !Z(this, c, b, l)) return !1;
          }

          for (f in j) {
            (g = h[f]) && (m && (g.f ? g.t[g.p](g.s) : g.t[g.p] = g.s, i = !0), g.pg && g.t._kill(j) && (i = !0), g.pg && 0 !== g.t._overwriteProps.length || (g._prev ? g._prev._next = g._next : g === this._firstPT && (this._firstPT = g._next), g._next && (g._next._prev = g._prev), g._next = g._prev = null), delete h[f]), k && (e[f] = 1);
          }

          !this._firstPT && this._initted && this._enabled(!1, !1);
        }
      }
      return i;
    }, f.invalidate = function () {
      return this._notifyPluginsOfEnabled && F._onPluginEvent("_onDisable", this), this._firstPT = this._overwrittenProps = this._startAt = this._onUpdate = null, this._notifyPluginsOfEnabled = this._active = this._lazy = !1, this._propLookup = this._targets ? {} : [], C.prototype.invalidate.call(this), this.vars.immediateRender && (this._time = -k, this.render(Math.min(0, -this._delay))), this;
    }, f._enabled = function (a, b) {
      if (h || g.wake(), a && this._gc) {
        var c,
            d = this._targets;
        if (d) for (c = d.length; --c > -1;) {
          this._siblings[c] = Y(d[c], this, !0);
        } else this._siblings = Y(this.target, this, !0);
      }

      return C.prototype._enabled.call(this, a, b), this._notifyPluginsOfEnabled && this._firstPT ? F._onPluginEvent(a ? "_onEnable" : "_onDisable", this) : !1;
    }, F.to = function (a, b, c) {
      return new F(a, b, c);
    }, F.from = function (a, b, c) {
      return c.runBackwards = !0, c.immediateRender = 0 != c.immediateRender, new F(a, b, c);
    }, F.fromTo = function (a, b, c, d) {
      return d.startAt = c, d.immediateRender = 0 != d.immediateRender && 0 != c.immediateRender, new F(a, b, d);
    }, F.delayedCall = function (a, b, c, d, e) {
      return new F(b, 0, {
        delay: a,
        onComplete: b,
        onCompleteParams: c,
        callbackScope: d,
        onReverseComplete: b,
        onReverseCompleteParams: c,
        immediateRender: !1,
        lazy: !1,
        useFrames: e,
        overwrite: 0
      });
    }, F.set = function (a, b) {
      return new F(a, 0, b);
    }, F.getTweensOf = function (a, b) {
      if (null == a) return [];
      a = "string" != typeof a ? a : F.selector(a) || a;
      var c, d, e, f;

      if ((n(a) || G(a)) && "number" != typeof a[0]) {
        for (c = a.length, d = []; --c > -1;) {
          d = d.concat(F.getTweensOf(a[c], b));
        }

        for (c = d.length; --c > -1;) {
          for (f = d[c], e = c; --e > -1;) {
            f === d[e] && d.splice(c, 1);
          }
        }
      } else for (d = Y(a).concat(), c = d.length; --c > -1;) {
        (d[c]._gc || b && !d[c].isActive()) && d.splice(c, 1);
      }

      return d;
    }, F.killTweensOf = F.killDelayedCallsTo = function (a, b, c) {
      "object" == _typeof(b) && (c = b, b = !1);

      for (var d = F.getTweensOf(a, b), e = d.length; --e > -1;) {
        d[e]._kill(c, a);
      }
    };
    var aa = r("plugins.TweenPlugin", function (a, b) {
      this._overwriteProps = (a || "").split(","), this._propName = this._overwriteProps[0], this._priority = b || 0, this._super = aa.prototype;
    }, !0);

    if (f = aa.prototype, aa.version = "1.18.0", aa.API = 2, f._firstPT = null, f._addTween = N, f.setRatio = L, f._kill = function (a) {
      var b,
          c = this._overwriteProps,
          d = this._firstPT;
      if (null != a[this._propName]) this._overwriteProps = [];else for (b = c.length; --b > -1;) {
        null != a[c[b]] && c.splice(b, 1);
      }

      for (; d;) {
        null != a[d.n] && (d._next && (d._next._prev = d._prev), d._prev ? (d._prev._next = d._next, d._prev = null) : this._firstPT === d && (this._firstPT = d._next)), d = d._next;
      }

      return !1;
    }, f._roundProps = function (a, b) {
      for (var c = this._firstPT; c;) {
        (a[this._propName] || null != c.n && a[c.n.split(this._propName + "_").join("")]) && (c.r = b), c = c._next;
      }
    }, F._onPluginEvent = function (a, b) {
      var c,
          d,
          e,
          f,
          g,
          h = b._firstPT;

      if ("_onInitAllProps" === a) {
        for (; h;) {
          for (g = h._next, d = e; d && d.pr > h.pr;) {
            d = d._next;
          }

          (h._prev = d ? d._prev : f) ? h._prev._next = h : e = h, (h._next = d) ? d._prev = h : f = h, h = g;
        }

        h = b._firstPT = e;
      }

      for (; h;) {
        h.pg && "function" == typeof h.t[a] && h.t[a]() && (c = !0), h = h._next;
      }

      return c;
    }, aa.activate = function (a) {
      for (var b = a.length; --b > -1;) {
        a[b].API === aa.API && (P[new a[b]()._propName] = a[b]);
      }

      return !0;
    }, q.plugin = function (a) {
      if (!(a && a.propName && a.init && a.API)) throw "illegal plugin definition.";
      var b,
          c = a.propName,
          d = a.priority || 0,
          e = a.overwriteProps,
          f = {
        init: "_onInitTween",
        set: "setRatio",
        kill: "_kill",
        round: "_roundProps",
        initAll: "_onInitAllProps"
      },
          g = r("plugins." + c.charAt(0).toUpperCase() + c.substr(1) + "Plugin", function () {
        aa.call(this, c, d), this._overwriteProps = e || [];
      }, a.global === !0),
          h = g.prototype = new aa(c);
      h.constructor = g, g.API = a.API;

      for (b in f) {
        "function" == typeof a[b] && (h[f[b]] = a[b]);
      }

      return g.version = a.version, aa.activate([g]), g;
    }, d = a._gsQueue) {
      for (e = 0; e < d.length; e++) {
        d[e]();
      }

      for (f in o) {
        o[f].func || a.console.log("GSAP encountered missing dependency: com.greensock." + f);
      }
    }

    h = !1;
  }
}("undefined" != typeof module && module.exports && "undefined" != typeof global ? global : void 0 || window, "TweenLite");
//# sourceMappingURL=twinlite.js.map
