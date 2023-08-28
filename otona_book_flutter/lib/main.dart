import 'package:flutter/material.dart';
import 'package:otona_book_flutter/common/global.dart';
import 'package:otona_book_flutter/routes/index.dart';

void main() => Global.init().then((e) => runApp(const MainApp()));

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: const ReleasedFilmsRoute(),
      routes: <String, WidgetBuilder>{
        "login": (context) => const LoginRoute(),
        "released_films": (context) => const ReleasedFilmsRoute(),
        "homepage":(context) => const HomepageRoute()
      },
    );
  }
}
