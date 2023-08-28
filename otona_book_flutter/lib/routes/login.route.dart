import 'package:flutter/material.dart';

class LoginRoute extends StatefulWidget {
  const LoginRoute({super.key});

  @override
  State<StatefulWidget> createState() => _LoginRouteState();
}

class _LoginRouteState extends State<LoginRoute> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
          child: Column(
        children: [
          TextFormField(
            decoration: const InputDecoration(hintText: 'username:'),
          ),
          const SizedBox(
            height: 40.0,
          )
        ],
      )),
    );
  }
}
