import 'dart:math';

import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:otona_book_flutter/models/index.dart';

class FilmItem extends StatefulWidget {
  const FilmItem(this.film) : super(key: const ValueKey<int>(4));

  final Film film;

  @override
  State<StatefulWidget> createState() => _FilmItemState();
}

class _FilmItemState extends State<FilmItem> {
  final DateFormat dateFormatter = DateFormat('y-M-d');
  final covers = <String>[
    "https://s1.ax1x.com/2023/08/28/pPaMYtO.jpg",
    "https://s1.ax1x.com/2023/04/24/p9mItN8.png"
  ];

  @override
  Widget build(BuildContext context) {
    var bangoText = widget.film.bango;
    Random r = Random();
    final index = r.nextInt(covers.length);

    var cover_image = covers[index];
    if (widget.film.cover_images != null &&
        widget.film.cover_images!.isNotEmpty) {
      cover_image = widget.film.cover_images![0];
      cover_image = "http://localhost:9000/otona-book$cover_image";
    }
    if (widget.film.published_at != null) {
      // print(dateFormatter.format(widget.film.published_at!));
      bangoText += " / ${dateFormatter.format(DateTime.now())}";
    }
    return Card(
        color: Colors.white,
        child: Container(
          padding: const EdgeInsets.all(5.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              Container(
                padding: const EdgeInsets.symmetric(vertical: 5.0),
              ),
              Container(
                // color: Colors.grey,
                padding: const EdgeInsets.symmetric(horizontal: .5),
                child: CachedNetworkImage(
                  imageUrl: cover_image,
                ),
              ),
              Container(
                padding: const EdgeInsets.symmetric(horizontal: 10.0),
                child: Text(
                  widget.film.title,
                  maxLines: 3,
                ),
              ),
              Container(
                padding:
                    const EdgeInsets.symmetric(horizontal: 10.0, vertical: 3.0),
                child: Text(bangoText),
              )
            ],
          ),
        ));
  }
}
