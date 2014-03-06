using System;
using System.Collections.Generic;
using System.Text;

namespace Infragistics.Documents.Excel.Serialization.Excel2007.XLSB
{
	internal enum XLSBRecordType
	{
		BRTROWHDR = 0,
		BRTCELLBLANK = 1,
		BRTCELLRK = 2,
		BRTCELLERROR = 3,
		BRTCELLBOOL = 4,
		BRTCELLREAL = 5,
		BRTCELLST = 6,
		BRTCELLISST = 7,
		BRTFMLASTRING = 8,
		BRTFMLANUM = 9,
		BRTFMLABOOL = 10,
		BRTFMLAERROR = 11,
		BRTCELL0BLANK = 12,
		BRTCELL0RK = 13,
		BRTCELL0BOOL = 15,
		BRTCELL0REAL = 16,
		BRTCELL0ST = 17,
		BRTCELL0ISST = 18,
		BRTSSTITEM = 19,
		BRTPCDIMISSING = 20,
		BRTPCDINUMBER = 21,
		BRTPCDIBOOLEAN = 22,
		BRTPCDIERROR = 23,
		BRTPCDISTRING = 24,
		BRTPCDIDATETIME = 25,
		BRTPCDIINDEX = 26,
		BRTPCDIAMISSING = 27,
		BRTPCDIANUMBER = 28,
		BRTPCDIABOOLEAN = 29,
		BRTPCDIAERROR = 30,
		BRTPCDIASTRING = 31,
		BRTPCDIADATETIME = 32,
		BRTPCRRECORD = 33,
		BRTPCRRECORDDT = 34,
		BRTFRTBEGIN = 35,
		BRTFRTEND = 36,
		BRTACBEGIN = 37,
		BRTACEND = 38,
		BRTNAME = 39,
		BRTINDEXBLOCK = 40,
		BRTINDEXROWBLOCK = 42,
		BRTFONT = 43,
		BRTFMT = 44,
		BRTFILL = 45,
		BRTBORDER = 46,
		BRTXF = 47,
		BRTSTYLE = 48,
		BRTCELLMETA = 49,
		BRTVALUEMETA = 50,
		BRTMDB = 51,
		BRTBEGINFMD = 52,
		BRTENDFMD = 53,
		BRTBEGINMDX = 54,
		BRTENDMDX = 55,
		BRTBEGINMDXTUPLE = 56,
		BRTENDMDXTUPLE = 57,
		BRTMDXMBRISTR = 58,
		BRTSTR = 59,
		BRTCOLINFO = 60,
		BRTCELL0RSTRING = 61,
		BRTCELLRSTRING = 62,
		BRTCALCCELL = 63,
		BRTDVAL = 64,
		BRTFILEVERSION = 128,
		BRTBEGINSHEET = 129,
		BRTENDSHEET = 130,
		BRTBEGINBOOK = 131,
		BRTENDBOOK = 132,
		BRTBEGINWSVIEWS = 133,
		BRTENDWSVIEWS = 134,
		BRTBEGINBOOKVIEWS = 135,
		BRTENDBOOKVIEWS = 136,
		BRTBEGINWSVIEW = 137,
		BRTENDWSVIEW = 138,
		BRTBEGINCSVIEWS = 139,
		BRTENDCSVIEWS = 140,
		BRTBEGINCSVIEW = 141,
		BRTENDCSVIEW = 142,
		BRTBEGINBUNDLESHS = 143,
		BRTENDBUNDLESHS = 144,
		BRTBEGINSHEETDATA = 145,
		BRTENDSHEETDATA = 146,
		BRTWSPROP = 147,
		BRTWSDIM = 148,
		BRTPANE = 151,
		BRTSEL = 152,
		BRTWBPROP = 153,
		BRTWBFACTOID = 154,
		BRTFILERECOVER = 155,
		BRTBUNDLESH = 156,
		BRTCALCPROP = 157,
		BRTBOOKVIEW = 158,
		BRTBEGINSST = 159,
		BRTENDSST = 160,
		BRTBEGINAFILTER = 161,
		BRTENDAFILTER = 162,
		BRTBEGINFILTERCOLUMN = 163,
		BRTENDFILTERCOLUMN = 164,
		BRTBEGINFILTERS = 165,
		BRTENDFILTERS = 166,
		BRTFILTER = 167,
		BRTCOLORFILTER = 168,
		BRTICONFILTER = 169,
		BRTTOP10FILTER = 170,
		BRTDYNAMICFILTER = 171,
		BRTBEGINCUSTOMFILTERS = 172,
		BRTENDCUSTOMFILTERS = 173,
		BRTCUSTOMFILTER = 174,
		BRTAFILTERDATEGROUPITEM = 175,
		BRTMERGECELL = 176,
		BRTBEGINMERGECELLS = 177,
		BRTENDMERGECELLS = 178,
		BRTBEGINPIVOTCACHEDEF = 179,
		BRTENDPIVOTCACHEDEF = 180,
		BRTBEGINPCDFIELDS = 181,
		BRTENDPCDFIELDS = 182,
		BRTBEGINPCDFIELD = 183,
		BRTENDPCDFIELD = 184,
		BRTBEGINPCDSOURCE = 185,
		BRTENDPCDSOURCE = 186,
		BRTBEGINPCDSRANGE = 187,
		BRTENDPCDSRANGE = 188,
		BRTBEGINPCDFATBL = 189,
		BRTENDPCDFATBL = 190,
		BRTBEGINPCDIRUN = 191,
		BRTENDPCDIRUN = 192,
		BRTBEGINPIVOTCACHERECORDS = 193,
		BRTENDPIVOTCACHERECORDS = 194,
		BRTBEGINPCDHIERARCHIES = 195,
		BRTENDPCDHIERARCHIES = 196,
		BRTBEGINPCDHIERARCHY = 197,
		BRTENDPCDHIERARCHY = 198,
		BRTBEGINPCDHFIELDSUSAGE = 199,
		BRTENDPCDHFIELDSUSAGE = 200,
		BRTBEGINEXTCONNECTION = 201,
		BRTENDEXTCONNECTION = 202,
		BRTBEGINECDBPROPS = 203,
		BRTENDECDBPROPS = 204,
		BRTBEGINECOLAPPROPS = 205,
		BRTENDECOLAPPROPS = 206,
		BRTBEGINPCDSCONSOL = 207,
		BRTENDPCDSCONSOL = 208,
		BRTBEGINPCDSCPAGES = 209,
		BRTENDPCDSCPAGES = 210,
		BRTBEGINPCDSCPAGE = 211,
		BRTENDPCDSCPAGE = 212,
		BRTBEGINPCDSCPITEM = 213,
		BRTENDPCDSCPITEM = 214,
		BRTBEGINPCDSCSETS = 215,
		BRTENDPCDSCSETS = 216,
		BRTBEGINPCDSCSET = 217,
		BRTENDPCDSCSET = 218,
		BRTBEGINPCDFGROUP = 219,
		BRTENDPCDFGROUP = 220,
		BRTBEGINPCDFGITEMS = 221,
		BRTENDPCDFGITEMS = 222,
		BRTBEGINPCDFGRANGE = 223,
		BRTENDPCDFGRANGE = 224,
		BRTBEGINPCDFGDISCRETE = 225,
		BRTENDPCDFGDISCRETE = 226,
		BRTBEGINPCDSDTUPLECACHE = 227,
		BRTENDPCDSDTUPLECACHE = 228,
		BRTBEGINPCDSDTCENTRIES = 229,
		BRTENDPCDSDTCENTRIES = 230,
		BRTBEGINPCDSDTCEMEMBERS = 231,
		BRTENDPCDSDTCEMEMBERS = 232,
		BRTBEGINPCDSDTCEMEMBER = 233,
		BRTENDPCDSDTCEMEMBER = 234,
		BRTBEGINPCDSDTCQUERIES = 235,
		BRTENDPCDSDTCQUERIES = 236,
		BRTBEGINPCDSDTCQUERY = 237,
		BRTENDPCDSDTCQUERY = 238,
		BRTBEGINPCDSDTCSETS = 239,
		BRTENDPCDSDTCSETS = 240,
		BRTBEGINPCDSDTCSET = 241,
		BRTENDPCDSDTCSET = 242,
		BRTBEGINPCDCALCITEMS = 243,
		BRTENDPCDCALCITEMS = 244,
		BRTBEGINPCDCALCITEM = 245,
		BRTENDPCDCALCITEM = 246,
		BRTBEGINPRULE = 247,
		BRTENDPRULE = 248,
		BRTBEGINPRFILTERS = 249,
		BRTENDPRFILTERS = 250,
		BRTBEGINPRFILTER = 251,
		BRTENDPRFILTER = 252,
		BRTBEGINPNAMES = 253,
		BRTENDPNAMES = 254,
		BRTBEGINPNAME = 255,
		BRTENDNAME = 256,
		BRTBEGINPNPAIRS = 257,
		BRTENDPNPAIRS = 258,
		BRTBEGINPNPAIR = 259,
		BRTENDPNPAIR = 260,
		BRTBEGINECWEBPROPS = 261,
		BRTENDECWEBPROPS = 262,
		BRTBEGINECWPTABLES = 263,
		BRTENDECWPTABLES = 264,
		BRTBEGINECPARAMS = 265,
		BRTENDECPARAMS = 266,
		BRTBEGINECPARAM = 267,
		BRTENDECPARAM = 268,
		BRTBEGINPCDKPIS = 269,
		BRTENDPCDKPIS = 270,
		BRTBEGINPCDKPI = 271,
		BRTENDPCDKPI = 272,
		BRTBEGINDIMS = 273,
		BRTENDDIMS = 274,
		BRTBEGINDIM = 275,
		BRTENDDIM = 276,
		BRTINDEXPARTEND = 277,
		BRTBEGINSTYLESHEET = 278,
		BRTENDSTYLESHEET = 279,
		BRTBEGINSXVIEW = 280,
		BRTENDSXVI = 281,
		BRTBEGINSXVI = 282,
		BRTBEGINSXVIS = 283,
		BRTENDSXVIS = 284,
		BRTBEGINSXVD = 285,
		BRTENDSXVD = 286,
		BRTBEGINSXVDS = 287,
		BRTENDSXVDS = 288,
		BRTBEGINSXPI = 289,
		BRTENDSXPI = 290,
		BRTBEGINSXPIS = 291,
		BRTENDSXPIS = 292,
		BRTBEGINSXDI = 293,
		BRTENDSXDI = 294,
		BRTBEGINSXDIS = 295,
		BRTENDSXDIS = 296,
		BRTBEGINSXLI = 297,
		BRTENDSXLI = 298,
		BRTBEGINSXLIRWS = 299,
		BRTENDSXLIRWS = 300,
		BRTBEGINSXLICOLS = 301,
		BRTENDSXLICOLS = 302,
		BRTBEGINSXFORMAT = 303,
		BRTENDSXFORMAT = 304,
		BRTBEGINSXFORMATS = 305,
		BRTENDSXFORMATS = 306,
		BRTBEGINSXSELECT = 307,
		BRTENDSXSELECT = 308,
		BRTBEGINISXVDRWS = 309,
		BRTENDISXVDRWS = 310,
		BRTBEGINISXVDCOLS = 311,
		BRTENDISXVDCOLS = 312,
		BRTENDSXLOCATION = 313,
		BRTBEGINSXLOCATION = 314,
		BRTENDSXVIEW = 315,
		BRTBEGINSXTHS = 316,
		BRTENDSXTHS = 317,
		BRTBEGINSXTH = 318,
		BRTENDSXTH = 319,
		BRTBEGINISXTHRWS = 320,
		BRTENDISXTHRWS = 321,
		BRTBEGINISXTHCOLS = 322,
		BRTENDISXTHCOLS = 323,
		BRTBEGINSXTDMPS = 324,
		BRTENDSXTDMPS = 325,
		BRTBEGINSXTDMP = 326,
		BRTENDSXTDMP = 327,
		BRTBEGINSXTHITEMS = 328,
		BRTENDSXTHITEMS = 329,
		BRTBEGINSXTHITEM = 330,
		BRTENDSXTHITEM = 331,
		BRTBEGINMETADATA = 332,
		BRTENDMETADATA = 333,
		BRTBEGINESMDTINFO = 334,
		BRTMDTINFO = 335,
		BRTENDESMDTINFO = 336,
		BRTBEGINESMDB = 337,
		BRTENDESMDB = 338,
		BRTBEGINESFMD = 339,
		BRTENDESFMD = 340,
		BRTBEGINSINGLECELLS = 341,
		BRTENDSINGLECELLS = 342,
		BRTBEGINTABLE = 343,
		BRTENDTABLE = 344,
		BRTBEGINTABLECOLS = 345,
		BRTENDTABLECOLS = 346,
		BRTBEGINTABLECOL = 347,
		BRTENDTABLECOL = 348,
		BRTBEGINTABLEXMLCPR = 349,
		BRTENDTABLEXMLCPR = 350,
		BRTTABLECCFMLA = 351,
		BRTTABLETRFMLA = 352,
		BRTBEGINEXTERNALS = 353,
		BRTENDEXTERNALS = 354,
		BRTSUPBOOKSRC = 355,
		BRTSUPSELF = 357,
		BRTSUPSAME = 358,
		BRTSUPTABS = 359,
		BRTBEGINSUPBOOK = 360,
		BRTPLACEHOLDERNAME = 361,
		BRTEXTERNSHEET = 362,
		BRTEXTERNTABLESTART = 363,
		BRTEXTERNTABLEEND = 364,
		BRTEXTERNROWHDR = 366,
		BRTEXTERNCELLBLANK = 367,
		BRTEXTERNCELLREAL = 368,
		BRTEXTERNCELLBOOL = 369,
		BRTEXTERNCELLERROR = 370,
		BRTEXTERNCELLSTRING = 371,
		BRTBEGINESMDX = 372,
		BRTENDESMDX = 373,
		BRTBEGINMDXSET = 374,
		BRTENDMDXSET = 375,
		BRTBEGINMDXMBRPROP = 376,
		BRTENDMDXMBRPROP = 377,
		BRTBEGINMDXKPI = 378,
		BRTENDMDXKPI = 379,
		BRTBEGINESSTR = 380,
		BRTENDESSTR = 381,
		BRTBEGINPRFITEM = 382,
		BRTENDPRFITEM = 383,
		BRTBEGINPIVOTCACHEIDS = 384,
		BRTENDPIVOTCACHEIDS = 385,
		BRTBEGINPIVOTCACHEID = 386,
		BRTENDPIVOTCACHEID = 387,
		BRTBEGINISXVIS = 388,
		BRTENDISXVIS = 389,
		BRTBEGINCOLINFOS = 390,
		BRTENDCOLINFOS = 391,
		BRTBEGINRWBRK = 392,
		BRTENDRWBRK = 393,
		BRTBEGINCOLBRK = 394,
		BRTENDCOLBRK = 395,
		BRTBRK = 396,
		BRTUSERBOOKVIEW = 397,
		BRTINFO = 398,
		BRTCUSR = 399,
		BRTUSR = 400,
		BRTBEGINUSERS = 401,
		BRTEOF = 403,
		BRTUCR = 404,
		BRTRRINSDEL = 405,
		BRTRRENDINSDEL = 406,
		BRTRRMOVE = 407,
		BRTRRENDMOVE = 408,
		BRTRRCHGCELL = 409,
		BRTRRENDCHGCELL = 410,
		BRTRRHEADER = 411,
		BRTRRUSERVIEW = 412,
		BRTRRRENSHEET = 413,
		BRTRRINSERTSH = 414,
		BRTRRDEFNAME = 415,
		BRTRRNOTE = 416,
		BRTRRCONFLICT = 417,
		BRTRRTQSIF = 418,
		BRTRRFORMAT = 419,
		BRTRRENDFORMAT = 420,
		BRTRRAUTOFMT = 421,
		BRTBEGINUSERSHVIEWS = 422,
		BRTBEGINUSERSHVIEW = 423,
		BRTENDUSERSHVIEW = 424,
		BRTENDUSERSHVIEWS = 425,
		BRTARRFMLA = 426,
		BRTSHRFMLA = 427,
		BRTTABLE = 428,
		BRTBEGINEXTCONNECTIONS = 429,
		BRTENDEXTCONNECTIONS = 430,
		BRTBEGINPCDCALCMEMS = 431,
		BRTENDPCDCALCMEMS = 432,
		BRTBEGINPCDCALCMEM = 433,
		BRTENDPCDCALCMEM = 434,
		BRTBEGINPCDHGLEVELS = 435,
		BRTENDPCDHGLEVELS = 436,
		BRTBEGINPCDHGLEVEL = 437,
		BRTENDPCDHGLEVEL = 438,
		BRTBEGINPCDHGLGROUPS = 439,
		BRTENDPCDHGLGROUPS = 440,
		BRTBEGINPCDHGLGROUP = 441,
		BRTENDPCDHGLGROUP = 442,
		BRTBEGINPCDHGLGMEMBERS = 443,
		BRTENDPCDHGLGMEMBERS = 444,
		BRTBEGINPCDHGLGMEMBER = 445,
		BRTENDPCDHGLGMEMBER = 446,
		BRTBEGINQSI = 447,
		BRTENDQSI = 448,
		BRTBEGINQSIR = 449,
		BRTENDQSIR = 450,
		BRTBEGINDELETEDNAMES = 451,
		BRTENDDELETEDNAMES = 452,
		BRTBEGINDELETEDNAME = 453,
		BRTENDDELETEDNAME = 454,
		BRTBEGINQSIFS = 455,
		BRTENDQSIFS = 456,
		BRTBEGINQSIF = 457,
		BRTENDQSIF = 458,
		BRTBEGINAUTOSORTSCOPE = 459,
		BRTENDAUTOSORTSCOPE = 460,
		BRTBEGINCONDITIONALFORMATTING = 461,
		BRTENDCONDITIONALFORMATTING = 462,
		BRTBEGINCFRULE = 463,
		BRTENDCFRULE = 464,
		BRTBEGINICONSET = 465,
		BRTENDICONSET = 466,
		BRTBEGINDATABAR = 467,
		BRTENDDATABAR = 468,
		BRTBEGINCOLORSCALE = 469,
		BRTENDCOLORSCALE = 470,
		BRTCFVO = 471,
		BRTEXTERNVALUEMETA = 472,
		BRTBEGINCOLORPALETTE = 473,
		BRTENDCOLORPALETTE = 474,
		BRTINDEXEDCOLOR = 475,
		BRTMARGINS = 476,
		BRTPRINTOPTIONS = 477,
		BRTPAGESETUP = 478,
		BRTBEGINHEADERFOOTER = 479,
		BRTENDHEADERFOOTER = 480,
		BRTBEGINSXCRTFORMAT = 481,
		BRTENDSXCRTFORMAT = 482,
		BRTBEGINSXCRTFORMATS = 483,
		BRTENDSXCRTFORMATS = 484,
		BRTWSFMTINFO = 485,
		BRTBEGINMGS = 486,
		BRTENDMGS = 487,
		BRTBEGINMGMAPS = 488,
		BRTENDMGMAPS = 489,
		BRTBEGINMG = 490,
		BRTENDMG = 491,
		BRTBEGINMAP = 492,
		BRTENDMAP = 493,
		BRTHLINK = 494,
		BRTBEGINDCON = 495,
		BRTENDDCON = 496,
		BRTBEGINDREFS = 497,
		BRTENDDREFS = 498,
		BRTDREF = 499,
		BRTBEGINSCENMAN = 500,
		BRTENDSCENMAN = 501,
		BRTBEGINSCT = 502,
		BRTENDSCT = 503,
		BRTSLC = 504,
		BRTBEGINDXFS = 505,
		BRTENDDXFS = 506,
		BRTDXF = 507,
		BRTBEGINTABLESTYLES = 508,
		BRTENDTABLESTYLES = 509,
		BRTBEGINTABLESTYLE = 510,
		BRTENDTABLESTYLE = 511,
		BRTTABLESTYLEELEMENT = 512,
		BRTTABLESTYLECLIENT = 513,
		BRTBEGINVOLDEPS = 514,
		BRTENDVOLDEPS = 515,
		BRTBEGINVOLTYPE = 516,
		BRTENDVOLTYPE = 517,
		BRTBEGINVOLMAIN = 518,
		BRTENDVOLMAIN = 519,
		BRTBEGINVOLTOPIC = 520,
		BRTENDVOLTOPIC = 521,
		BRTVOLSUBTOPIC = 522,
		BRTVOLREF = 523,
		BRTVOLNUM = 524,
		BRTVOLERR = 525,
		BRTVOLSTR = 526,
		BRTVOLBOOL = 527,
		BRTBEGINCALCCHAIN = 528,
		BRTENDCALCCHAIN = 529,
		BRTBEGINSORTSTATE = 530,
		BRTENDSORTSTATE = 531,
		BRTBEGINSORTCOND = 532,
		BRTENDSORTCOND = 533,
		BRTBOOKPROTECTION = 534,
		BRTSHEETPROTECTION = 535,
		BRTRANGEPROTECTION = 536,
		BRTPHONETICINFO = 537,
		BRTBEGINECTXTWIZ = 538,
		BRTENDECTXTWIZ = 539,
		BRTBEGINECTWFLDINFOLST = 540,
		BRTENDECTWFLDINFOLST = 541,
		BRTBEGINECTWFLDINFO = 542,
		BRTFILESHARING = 548,
		BRTOLESIZE = 549,
		BRTDRAWING = 550,
		BRTLEGACYDRAWING = 551,
		BRTLEGACYDRAWINGHF = 552,
		BRTWEBOPT = 553,
		BRTBEGINWEBPUBITEMS = 554,
		BRTENDWEBPUBITEMS = 555,
		BRTBEGINWEBPUBITEM = 556,
		BRTENDWEBPUBITEM = 557,
		BRTBEGINSXCONDFMT = 558,
		BRTENDSXCONDFMT = 559,
		BRTBEGINSXCONDFMTS = 560,
		BRTENDSXCONDFMTS = 561,
		BRTBKHIM = 562,
		BRTCOLOR = 564,
		BRTBEGININDEXEDCOLORS = 565,
		BRTENDINDEXEDCOLORS = 566,
		BRTBEGINMRUCOLORS = 569,
		BRTENDMRUCOLORS = 570,
		BRTMRUCOLOR = 572,
		BRTBEGINDVALS = 573,
		BRTENDDVALS = 574,
		BRTSUPNAMESTART = 577,
		BRTSUPNAMEVALUESTART = 578,
		BRTSUPNAMEVALUEEND = 579,
		BRTSUPNAMENUM = 580,
		BRTSUPNAMEERR = 581,
		BRTSUPNAMEST = 582,
		BRTSUPNAMENIL = 583,
		BRTSUPNAMEBOOL = 584,
		BRTSUPNAMEFMLA = 585,
		BRTSUPNAMEBITS = 586,
		BRTSUPNAMEEND = 587,
		BRTENDSUPBOOK = 588,
		BRTCELLSMARTTAGPROPERTY = 589,
		BRTBEGINCELLSMARTTAG = 590,
		BRTENDCELLSMARTTAG = 591,
		BRTBEGINCELLSMARTTAGS = 592,
		BRTENDCELLSMARTTAGS = 593,
		BRTBEGINSMARTTAGS = 594,
		BRTENDSMARTTAGS = 595,
		BRTSMARTTAGTYPE = 596,
		BRTBEGINSMARTTAGTYPES = 597,
		BRTENDSMARTTAGTYPES = 598,
		BRTBEGINSXFILTERS = 599,
		BRTENDSXFILTERS = 600,
		BRTBEGINSXFILTER = 601,
		BRTENDSXFILTER = 602,
		BRTBEGINFILLS = 603,
		BRTENDFILLS = 604,
		BRTBEGINCELLWATCHES = 605,
		BRTENDCELLWATCHES = 606,
		BRTCELLWATCH = 607,
		BRTBEGINCRERRS = 608,
		BRTENDCRERRS = 609,
		BRTCRASHRECERR = 610,
		BRTBEGINFONTS = 611,
		BRTENDFONTS = 612,
		BRTBEGINBORDERS = 613,
		BRTENDBORDERS = 614,
		BRTBEGINFMTS = 615,
		BRTENDFMTS = 616,
		BRTBEGINCELLXFS = 617,
		BRTENDCELLXFS = 618,
		BRTBEGINSTYLES = 619,
		BRTENDSTYLES = 620,
		BRTMUSTUNDERSTAND = 621,
		BRTBIGNAME = 625,
		BRTBEGINCELLSTYLEXFS = 626,
		BRTENDCELLSTYLEXFS = 627,
		BRTBEGINCOMMENTS = 628,
		BRTENDCOMMENTS = 629,
		BRTBEGINCOMMENTAUTHORS = 630,
		BRTENDCOMMENTAUTHORS = 631,
		BRTCOMMENTAUTHOR = 632,
		BRTBEGINCOMMENTLIST = 633,
		BRTENDCOMMENTLIST = 634,
		BRTBEGINCOMMENT = 635,
		BRTENDCOMMENT = 636,
		BRTCOMMENTTEXT = 637,
		BRTBEGINOLEOBJECTS = 638,
		BRTOLEOBJECT = 639,
		BRTENDOLEOBJECTS = 640,
		BRTBEGINSXRULES = 641,
		BRTENDSXRULES = 642,
		BRTBEGINACTIVEXCONTROLS = 643,
		BRTACTIVEX = 644,
		BRTENDACTIVEXCONTROLS = 645,
		BRTBEGINPCDSDTCEMEMBERSSORTBY = 646,
		BRTENDPCDSDTCEMEMBERSSORTBY = 647,
		BRTBEGINCELLIGNOREECS = 648,
		BRTCELLIGNOREEC = 649,
		BRTENDCELLIGNOREECS = 650,
		BRTCSPROP = 651,
		BRTCSPAGESETUP = 652,
		BRTBEGINUSERCSVIEWS = 653,
		BRTENDUSERCSVIEWS = 654,
		BRTBEGINUSERCSVIEW = 655,
		BRTENDUSERCSVIEW = 656,
		BRTBEGINPCDSFCIENTRIES = 657,
		BRTENDPCDSFCIENTRIES = 658,
		BRTPCDSFCIENTRY = 659,
		BRTBEGINTABLEPARTS = 660,
		BRTTABLEPART = 661,
		BRTENDTABLEPARTS = 662,
		BRTSHEETCALCPROP = 663,
		BRTBEGINFNGROUP = 664,
		BRTFNGROUP = 665,
		BRTENDFNGROUP = 666,
		BRTSUPADDIN = 667,
		BRTSXTDMPORDER = 668,
		BRTCSPROTECTION = 669,
	}
}

#region Copyright (c) 2001-2012 Infragistics, Inc. All Rights Reserved
/* ---------------------------------------------------------------------*
*                           Infragistics, Inc.                          *
*              Copyright (c) 2001-2012 All Rights reserved               *
*                                                                       *
*                                                                       *
* This file and its contents are protected by United States and         *
* International copyright laws.  Unauthorized reproduction and/or       *
* distribution of all or any portion of the code contained herein       *
* is strictly prohibited and will result in severe civil and criminal   *
* penalties.  Any violations of this copyright will be prosecuted       *
* to the fullest extent possible under law.                             *
*                                                                       *
* THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
* TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
* TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
* CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
* THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF INFRAGISTICS, INC. *
*                                                                       *
* UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
* PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME, OR  *
* SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY INFRAGISTICS PRODUCT.    *
*                                                                       *
* THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
* CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF INFRAGISTICS,      *
* INC.  THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO       *
* INSURE ITS CONFIDENTIALITY.                                           *
*                                                                       *
* THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
* PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
* EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
* THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
* SOURCE CODE CONTAINED HEREIN.                                         *
*                                                                       *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
* --------------------------------------------------------------------- *
*/
#endregion Copyright (c) 2001-2012 Infragistics, Inc. All Rights Reserved