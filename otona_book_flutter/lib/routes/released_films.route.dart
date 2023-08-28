import 'package:flutter/material.dart';
import 'package:flutter_staggered_grid_view/flutter_staggered_grid_view.dart';
import 'package:otona_book_flutter/common/api_client.dart';
import 'package:otona_book_flutter/models/film.dart';
import 'package:otona_book_flutter/widgets/film_item.dart';

class ReleasedFilmsRoute extends StatefulWidget {
  const ReleasedFilmsRoute({super.key});

  @override
  State<StatefulWidget> createState() => _ReleasedFilmsRoute();
}

class _ReleasedFilmsRoute extends State<ReleasedFilmsRoute> {
  late final ScrollController controller;
  bool isLoading = false;
  int pageNo = 0;
  final pageSize = 20;
  final films = <Film>[];

  Future<void> retrieveFilms() async {
    setState(() => isLoading = true);
    var data = await ApiClient(context)
        .post("/film/query-list", {"page_no": ++pageNo, "page_size": pageSize});
    data = (data as List<dynamic>);

    var list = data.map((i) => Film.fromJson(i)).toList();
    setState(() {
      films.addAll(list);
      isLoading = false;
      print("load done");
    });
  }

  void initController() {
    controller = ScrollController();
    controller.addListener(() {
      if (controller.position.pixels == controller.position.maxScrollExtent &&
          !isLoading) {
        print("load more");
        retrieveFilms();
      }
    });
  }

  @override
  void initState() {
    super.initState();
    initController();
    retrieveFilms();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      color: Colors.grey,
      padding: const EdgeInsets.all(10.0),
      child: MasonryGridView.builder(
        controller: controller,
        gridDelegate: const SliverSimpleGridDelegateWithFixedCrossAxisCount(
            crossAxisCount: 2),
        shrinkWrap: true,
        mainAxisSpacing: 10.0,
        crossAxisSpacing: 10.0,
        itemCount: films.length,
        itemBuilder: (context, index) {
          return FilmItem(films[index]);
        },
      ),
    );
  }
}
