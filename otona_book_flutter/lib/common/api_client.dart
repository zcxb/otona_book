import 'dart:io';

import 'package:dio/dio.dart';
import 'package:dio/io.dart';
import 'package:flutter/material.dart';
import 'package:otona_book_flutter/common/global.dart';
import 'package:otona_book_flutter/models/api_dto.dart';

class ApiClient {
  ApiClient([this.context]) {
    _options = Options(extra: {"context": context});
  }

  BuildContext? context;
  late Options _options;
  static Dio dio = Dio(BaseOptions(
      baseUrl: "http://localhost:7129",
      headers: {HttpHeaders.acceptHeader: "application/json"}));

  Future<dynamic> post(String route, Object? data) async {
    var response = await dio.post<dynamic>(route, data: data);
    var responseData = ApiResponse.fromJson(response.data);

    if (responseData.code != 0) {
      throw Error();
    }

    return responseData.data;
  }

  static void init() {
    if (!Global.isRelease) {
      (dio.httpClientAdapter as DefaultHttpClientAdapter).onHttpClientCreate =
          (client) {
        // client.findProxy = (uri) {
        //   return 'PROXY 192.168.50.154:8888';
        // };
        //代理工具会提供一个抓包的自签名证书，会通不过证书校验，所以我们禁用证书校验
        client.badCertificateCallback =
            (X509Certificate cert, String host, int port) => true;
      };
    }
  }
}
