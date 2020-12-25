//定义json库
"object" != typeof JSON && (JSON = {}), function () { "use strict"; function f(t) { return 10 > t ? "0" + t : t } function this_value() { return this.valueOf() } function quote(t) { return rx_escapable.lastIndex = 0, rx_escapable.test(t) ? '"' + t.replace(rx_escapable, function (t) { var e = meta[t]; return "string" == typeof e ? e : "\\u" + ("0000" + t.charCodeAt(0).toString(16)).slice(-4) }) + '"' : '"' + t + '"' } function str(t, e) { var r, n, o, u, f, a = gap, i = e[t]; switch (i && "object" == typeof i && "function" == typeof i.toJSON && (i = i.toJSON(t)), "function" == typeof rep && (i = rep.call(e, t, i)), typeof i) { case "string": return quote(i); case "number": return isFinite(i) ? i + "" : "null"; case "boolean": case "null": return i + ""; case "object": if (!i) return "null"; if (gap += indent, f = [], "[object Array]" === Object.prototype.toString.apply(i)) { for (u = i.length, r = 0; u > r; r += 1)f[r] = str(r, i) || "null"; return o = 0 === f.length ? "[]" : gap ? "[\n" + gap + f.join(",\n" + gap) + "\n" + a + "]" : "[" + f.join(",") + "]", gap = a, o } if (rep && "object" == typeof rep) for (u = rep.length, r = 0; u > r; r += 1)"string" == typeof rep[r] && (n = rep[r], o = str(n, i), o && f.push(quote(n) + (gap ? ": " : ":") + o)); else for (n in i) Object.prototype.hasOwnProperty.call(i, n) && (o = str(n, i), o && f.push(quote(n) + (gap ? ": " : ":") + o)); return o = 0 === f.length ? "{}" : gap ? "{\n" + gap + f.join(",\n" + gap) + "\n" + a + "}" : "{" + f.join(",") + "}", gap = a, o } } var rx_one = /^[\],:{}\s]*$/, rx_two = /\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, rx_three = /"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, rx_four = /(?:^|:|,)(?:\s*\[)+/g, rx_escapable = /[\\\"\u0000-\u001f\u007f-\u009f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, rx_dangerous = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g; "function" != typeof Date.prototype.toJSON && (Date.prototype.toJSON = function () { return isFinite(this.valueOf()) ? this.getUTCFullYear() + "-" + f(this.getUTCMonth() + 1) + "-" + f(this.getUTCDate()) + "T" + f(this.getUTCHours()) + ":" + f(this.getUTCMinutes()) + ":" + f(this.getUTCSeconds()) + "Z" : null }, Boolean.prototype.toJSON = this_value, Number.prototype.toJSON = this_value, String.prototype.toJSON = this_value); var gap, indent, meta, rep; "function" != typeof JSON.stringify && (meta = { "\b": "\\b", "	": "\\t", "\n": "\\n", "\f": "\\f", "\r": "\\r", '"': '\\"', "\\": "\\\\" }, JSON.stringify = function (t, e, r) { var n; if (gap = "", indent = "", "number" == typeof r) for (n = 0; r > n; n += 1)indent += " "; else "string" == typeof r && (indent = r); if (rep = e, e && "function" != typeof e && ("object" != typeof e || "number" != typeof e.length)) throw Error("JSON.stringify"); return str("", { "": t }) }), "function" != typeof JSON.parse && (JSON.parse = function (text, reviver) { function walk(t, e) { var r, n, o = t[e]; if (o && "object" == typeof o) for (r in o) Object.prototype.hasOwnProperty.call(o, r) && (n = walk(o, r), void 0 !== n ? o[r] = n : delete o[r]); return reviver.call(t, e, o) } var j; if (text += "", rx_dangerous.lastIndex = 0, rx_dangerous.test(text) && (text = text.replace(rx_dangerous, function (t) { return "\\u" + ("0000" + t.charCodeAt(0).toString(16)).slice(-4) })), rx_one.test(text.replace(rx_two, "@").replace(rx_three, "]").replace(rx_four, ""))) return j = eval("(" + text + ")"), "function" == typeof reviver ? walk({ "": j }, "") : j; throw new SyntaxError("JSON.parse") }) }();

var orginDoc;
var newDoc;
var outputDir;

var exportImg = true;

var curPicIndex;
var curDicNamedFileList;
var curCfgJson;
var curDicVarList;

const TAG_VAR = "=";
const TAG_DISCARD = "#";

const TAG_VAR_IMG = "=img";
const TAG_IMG = "img";
const TAG_ONLYIMG = "@img";

const TAG_TXT = "txt";

const TAG_VAR_BTN = "=btn";
const TAG_BTN = "btn";

function GetOutputNamePNG(name) {
    return outputDir + "/" + name + ".png";
}

function ExportPSD() {
    if (!documents.length) {
        alert("出错,请打开需要导出到unity的psd文档");
        return;
    }

    orginDoc = app.activeDocument;

    //从顶层图集开始，解析顶层里的wnd_或atlas_开头的窗口
    for (var i = 0; i < orginDoc.layerSets.length; i++) {
        var layer = orginDoc.layerSets[i];
        //规则:隐藏的就导出
        if (layer.visible == false) {
            continue;
        }
        //规则:如果以#开头,就不导出
        if (layer.name.indexOf(TAG_DISCARD) == 0) {
            continue;
        }

        var name = layer.name;
        if (name.indexOf("wnd_") == 0) {
            //开始解析对应的窗口
            ParseWindow(layer);
        }
        else if (name.indexOf("atlas_") == 0) {
            //开始解析对应的图集
            ParseAtlas(layer);
        }
    }

    alert(orginDoc.name + "已完成导出!");
}
//
function ParseWindow(window) {
    //先获取窗口名
    var nameList = window.name.split('_');
    if (nameList.length < 3) {
        alert("出错,命名错误" + window.name);
    }
    //分类
    var category = nameList[1];

    var categoryPath = orginDoc.path + "/../UI/" + category;
    var folder = Folder(categoryPath);
    //先创建分类目录
    if (folder.exists == false) {
        folder.create();
    }

    var curWndName = nameList[2];

    //先创建一个背景透明的新文档
    newDoc = app.documents.add(UnitValue("2000 px"), UnitValue("2000 px"), 72, "psd2ui-temp", NewDocumentMode.RGB, DocumentFill.TRANSPARENT);

    //还要再恢复到主文件成焦点
    app.activeDocument = orginDoc;

    //主动设置一下windowstype
    window.type = "wnd";

    //设置输出路径
    outputDir = categoryPath + "/" + curWndName;
    curDicNamedFileList = [];
    curCfgJson = {};
    curDicVarList = [];
    curCfgJson.name = window.name;
    curCfgJson.type = window.type;

    var folder = Folder(outputDir);
    if (folder.exists) {
        //存在文件夹
        //需要先记录一下老文件列
        var fileList = folder.getFiles();
        for (var i = fileList.length - 1; i >= 0; i--) {
            var name = fileList[i].name;
            //如果是meta或uicfg文件不记录
            if (name.lastIndexOf(".meta") > 0 || name.lastIndexOf(".uicfg") > 0) {
                continue;
            }

            name = name.substr(0, name.lastIndexOf('.'));
            //标记0为老文件
            curDicNamedFileList[name] = 0;
        }
    }
    else {
        //否则创建文件夹
        folder.create();
    }

    //设置默认命名计数器
    curPicIndex = 0;
    //开始解析当前window
    ParseLayerSet(window, curCfgJson);

    //销毁doc
    newDoc.close(SaveOptions.DONOTSAVECHANGES);

    //删除多余png
    for (var fileName in curDicNamedFileList) {
        if (curDicNamedFileList[fileName] == 0) {
            var file = new File(GetOutputNamePNG(fileName));
            file.remove();
        }
    }

    var jsonText = JSON.stringify(curCfgJson);
    //保存uicfg
    SaveText(outputDir + "/ui.uicfg", jsonText);
}

function ParseAtlas(atlas) {
    //先获取窗口名
    var nameList = atlas.name.split('_');
    if (nameList.length < 3) {
        alert("出错,命名错误" + atlas.name);
    }
    //分类
    var category = nameList[1];

    var categoryPath = orginDoc.path + "/../UI/" + category;
    var folder = Folder(categoryPath);
    //先创建分类目录
    if (folder.exists == false) {
        folder.create();
    }

    //先创建一个背景透明的新文档
    newDoc = app.documents.add(UnitValue("2000 px"), UnitValue("2000 px"), 72, "psd2ui-temp", NewDocumentMode.RGB, DocumentFill.TRANSPARENT);

    //还要再恢复到主文件成焦点
    app.activeDocument = orginDoc;

    //设置输出路径
    outputDir = categoryPath + "/" + nameList[2];
    curDicNamedFileList = [];
    curCfgJson = {};
    curDicVarList = [];

    var folder = Folder(outputDir);
    if (folder.exists) {
        //存在文件夹
        //需要先记录一下老文件列
        var fileList = folder.getFiles();
        for (var i = fileList.length - 1; i >= 0; i--) {
            var name = fileList[i].name;
            //如果是meta或uicfg文件不记录
            if (name.lastIndexOf(".meta") > 0 || name.lastIndexOf(".uicfg") > 0) {
                continue;
            }

            name = name.substr(0, name.lastIndexOf('.'));
            //标记0为老文件
            curDicNamedFileList[name] = 0;
        }
    }
    else {
        //否则创建文件夹
        folder.create();
    }

    //设置默认命名计数器
    curPicIndex = 0;
    //开始解析当前window
    ParseLayerSet(atlas, curCfgJson);

    //销毁doc
    newDoc.close(SaveOptions.DONOTSAVECHANGES);

    //删除多余png
    for (var fileName in curDicNamedFileList) {
        if (curDicNamedFileList[fileName] == 0) {
            var file = new File(GetOutputNamePNG(fileName));
            file.remove();
        }
    }
}

function ParseLayerSet(layer, jsonNode) {
    var layerName = layer.name;
    //规则: 组加上[=]btn就会认定为button
    if (layerName.indexOf(TAG_VAR_BTN) == 0 || layerName.indexOf(TAG_BTN) == 0) {
        jsonNode.type = "btn";
    }

    //否则就是普通的组
    //特殊=号开头就要记录var
    if (layerName.indexOf(TAG_VAR) == 0) {
        layerName = layerName.substr(1);
        //记录var
        if (curDicVarList[layerName]) {
            alert("出错,已经存在的变量:" + layerName);
        }
        else {
            curDicVarList[layerName] = 1;
            jsonNode.var = layerName;
        }
    }
    jsonNode.name = layerName;

    var bounds = layer.bounds;
    var v0 = bounds[0].value;
    var v1 = bounds[1].value;
    var v2 = bounds[2].value;
    var v3 = bounds[3].value;
    jsonNode.x = v0;
    jsonNode.y = v1;
    jsonNode.width = v2 - v0;
    jsonNode.height = v3 - v1;

    //如果有子节点
    if (layer.layers.length > 0) {
        jsonNode.nodes = [];
    }

    //按顺序遍历所有的组或层
    for (var i = 0; i < layer.layers.length; i++) {
        var childLayer = layer.layers[i];
        //if(childLayer.visible==false)
        //{
        //    continue;
        //}

        //如果是普通图层
        if (childLayer.typename != "LayerSet") {
            var childLayerName = childLayer.name;
            //规则:如果以#开头,就不导出
            if (childLayerName.indexOf(TAG_DISCARD) == 0) {
                continue;
            }

            //如果图层是文本类型
            if (childLayer.kind == LayerKind.TEXT) {
                //规则：组加上[=]img或@img就会文本框以图片导出
                if (childLayerName.indexOf(TAG_VAR_IMG) == 0 || childLayerName.indexOf(TAG_IMG) == 0 || childLayerName.indexOf(TAG_ONLYIMG) == 0) {
                    var childJsonNode = {};
                    //规则:普通图层以@img开头,就不存节点
                    if (childLayerName.indexOf(TAG_ONLYIMG) != 0) {
                        jsonNode.nodes.push(childJsonNode);
                    }

                    SaveOnePNG(childLayer, childJsonNode);
                }
                else {
                    //规则:否则就是普通的text
                    var childJsonNode = {};
                    jsonNode.nodes.push(childJsonNode);
                    SaveOneText(childLayer, childJsonNode);
                }
            }
            else//所有普通图层都打成一张图片
            {
                var childJsonNode = {};
                //规则:普通图层以@img开头,就不存节点
                if (childLayerName.indexOf(TAG_ONLYIMG) != 0) {
                    jsonNode.nodes.push(childJsonNode);
                }

                SaveOnePNG(childLayer, childJsonNode);

                //规则: 普通图层加上[=]btn,图片就会变成button
                if (childLayerName.indexOf(TAG_VAR_BTN) == 0 || childLayerName.indexOf(TAG_BTN) == 0) {
                    childJsonNode.type = "btn";
                }
            }
        }
        else//是组
        {
            var childLayerSet = childLayer;
            var childLayerSetName = childLayerSet.name;
            //规则:如果以#开头,就不导出
            if (childLayerSetName.indexOf(TAG_DISCARD) == 0) {
                continue;
            }

            var childLayerJson = {};
            //规则：组加上[=]img或@img就会直接整个组以图片导出
            if (childLayerSetName.indexOf(TAG_VAR_IMG) == 0 || childLayerSetName.indexOf(TAG_IMG) == 0 || childLayerSetName.indexOf(TAG_ONLYIMG) == 0) {
                //规则:普通图层以@img开头,就不存节点
                if (childLayerSetName.indexOf(TAG_ONLYIMG) != 0) {
                    jsonNode.nodes.push(childLayerJson);
                }

                SaveOnePNG(childLayerSet, childLayerJson);
            }
            else {
                jsonNode.nodes.push(childLayerJson);
                ParseLayerSet(childLayerSet, childLayerJson);
            }
        }
    }
}

//保存具体的图层
function SaveOnePNG(layer, jsonNode) {
    var layerName = layer.name;

    var variable = false;
    //是=号开头,就要生成变量名
    if (layerName.indexOf(TAG_VAR) == 0) {
        variable = true;
        layerName = layerName.substr(1);
        if (curDicVarList[layerName]) {
            alert("已经存在的变量:" + layerName);
        }
        else {
            curDicVarList[layerName] = 1;
            jsonNode.var = layerName;
        }
    }

    //是@img开头,要先删了@
    if (layerName.indexOf(TAG_ONLYIMG) == 0) {
        layerName = layerName.substr(1);
    }

    var pngName = layerName;
    //名字前面没有=号,并且不以img或pic开头,或者就是img或pic
    if ((variable == false && (layerName.indexOf(TAG_IMG) != 0 || layerName == TAG_IMG || layer.name == TAG_ONLYIMG)) || layer.name == TAG_VAR_IMG) {
        pngName = "img" + curPicIndex;
        curPicIndex++;
    }

    //不管怎么样，都把当前图层记录下来
    //json配置加一行
    jsonNode.name = layerName,
        jsonNode.type = "img";
    jsonNode.x = layer.bounds[0].value;
    jsonNode.y = layer.bounds[1].value;
    jsonNode.width = layer.bounds[2].value - layer.bounds[0].value;
    jsonNode.height = layer.bounds[3].value - layer.bounds[1].value;
    jsonNode.path = pngName + ".png";

    if (jsonNode.width == 0 || jsonNode.height == 0) {
        alert(layerName + "大小为0");
        return;
    }
    if (exportImg == false) {
        return;
    }
    //如果图片已经导出过，就不再导出
    if (curDicNamedFileList[pngName] == 1) {
        //alert("已经存在的文件:"+curDicNamedFileList[name]);
        return;
    }

    //标记1为新导出
    curDicNamedFileList[pngName] = 1;

    //把现有图层或组复制到新文档
    var newLayer = layer.duplicate(newDoc, ElementPlacement.INSIDE)
    //合并新文档
    app.activeDocument = newDoc;
    newDoc.resizeCanvas(jsonNode.width, jsonNode.height, AnchorPosition.TOPLEFT);
    newLayer.translate(-newLayer.bounds[0], -newLayer.bounds[1]);
    //这步不需要了
    //newDoc.mergeVisibleLayers();
    //裁剪为最小大小
    //newDoc.trim(TrimType.TRANSPARENT);
    //保存png
    SavePNG(newDoc, pngName);

    newLayer.remove();
    app.activeDocument = orginDoc;
}

function SaveOneText(layer, jsonNode) {
    var layerName = layer.name;

    //是=号开头,就要生成变量名
    if (layerName.indexOf(TAG_VAR) == 0) {
        layerName = layerName.substr(1);
        if (curDicVarList[layerName]) {
            alert("出错,已经存在的变量:" + layerName);
        }
        else {
            curDicVarList[layerName] = 1;
            jsonNode.var = layerName;
        }
    }

    jsonNode.name = layerName,
        jsonNode.type = "txt";

    var textItem = layer.textItem;
    jsonNode.x = textItem.position[0].value;
    jsonNode.y = textItem.position[1].value;
    if (textItem.kind = TextType.PARAGRAPHTEXT) {
        jsonNode.width = textItem.width.value;
        jsonNode.height = textItem.height.value;
    }
    else {
        var bounds = layer.bounds;
        var v0 = bounds[0].value;
        var v1 = bounds[1].value;
        var v2 = bounds[2].value;
        var v3 = bounds[3].value;
        jsonNode.width = v2 - v0;
        jsonNode.height = v3 - v1;
    }

    jsonNode.text = textItem.contents.replace(/\r\n/g, '__CRLF__').replace(/\r/g, '__CRLF__').replace(/\n/g, '__CRLF__').replace(/__CRLF__/g, '\r\n');
    jsonNode.font = textItem.font;
    //alert(jsonNode.text);
    //alert(textItem.size.value);
    jsonNode.size = textItem.size.value;
    try {
        jsonNode.align = textItem.justification.toString().slice(14).toLowerCase();
        jsonNode.color = "#" + textItem.color.rgb.hexValue;
    } catch (error) {
        e = error;
        jsonNode.align = 'left';
        jsonNode.color = "#000000";
    }
}

//保存png
function SavePNG(doc, pngName) {
    var fileName = GetOutputNamePNG(pngName);
    var file = new File(fileName);
    var options = new ExportOptionsSaveForWeb();
    options.format = SaveDocumentType.PNG;
    options.PNG8 = false;
    options.transparency = true;
    options.optimized = false;
    options.interlaced = false;
    doc.exportDocument(file, ExportType.SAVEFORWEB, options);
}

//保存text
function SaveText(filePath, text) {
    var file = File(filePath);
    file.encoding = "UTF8";
    file.open("w", "TEXT");
    file.write(text);
    return file.close();
};

var exportImageAndConfig = function (info) {
    exportImg = true;
    ExportPSD();
}