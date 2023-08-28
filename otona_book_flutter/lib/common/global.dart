import 'package:flutter/material.dart';
import 'package:otona_book_flutter/common/api_client.dart';

class Global {
  static bool get isRelease => const bool.fromEnvironment("dart.vm.product");
  static Future init() async {
    WidgetsFlutterBinding.ensureInitialized();

    ApiClient.init();
  }
}
