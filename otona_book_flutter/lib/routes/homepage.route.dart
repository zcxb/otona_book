import 'package:flutter/material.dart';

class HomepageRoute extends StatefulWidget {
  const HomepageRoute({super.key});

  @override
  State<StatefulWidget> createState() => _HomepageRoute();
}

class _HomepageRoute extends State<HomepageRoute> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Otona book"),
      ),
    );
  }
}
